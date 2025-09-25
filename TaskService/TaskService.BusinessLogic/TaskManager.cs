using NotificationService.ConnectionLib;
using NotificationService.ConnectionLib.Interfaces;
using Saritasa.Tools.Common.Pagination;
using TaskService.BusinessLogic.Interfaces;
using TaskService.Domain.Interfaces;
using Task = TaskService.Domain.Entities.Task;

namespace TaskService.BusinessLogic;

internal class TaskManager : ITaskManager
{
    private readonly Serilog.ILogger _logger;
    private readonly IUnitOfWork _unitOfWork;
    private readonly INotificationServiceConnection _notificationService;
    
    public TaskManager(IUnitOfWork unitOfWork, Serilog.ILogger logger, INotificationServiceConnection notificationService)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
        _notificationService = notificationService;
    }
    
    public async Task<Guid> CreateTaskAsync(
        string title,
        string? description,
        Guid creatorId,
        CancellationToken cancellationToken)
    {
        var task = Task.Create(title, description);
        await _unitOfWork.Tasks.AddAsync(task, cancellationToken);
        _logger.Information("Created task {@task}", task);
        return task.Id;
    }

    public async System.Threading.Tasks.Task DeleteTaskAsync(Guid id, CancellationToken cancellationToken)
    {
        var task = await GetTaskAsync(id, cancellationToken);
        await task.DeleteAsync(_unitOfWork, cancellationToken);
        _logger.Information("Deleted task {@task}", task);
    }

    public async Task<Task> GetTaskAsync(Guid id, CancellationToken cancellationToken)
    {
        var task = await _unitOfWork.Tasks.GetByIdAsync(id, cancellationToken);
        if (task.Deleted)
            throw new InvalidOperationException("Task is already deleted");
        return task;
    }

    public async System.Threading.Tasks.Task UpdateTaskAsync(
        Guid id,
        string title,
        string? description,
        CancellationToken cancellationToken
    )
    {
        const string notificationText = "Задача была обновлена";
        var task = await GetTaskAsync(id, cancellationToken);
        await task.UpdateAsync(title, description, _unitOfWork, cancellationToken);
        _logger.Information("Updated task: {@task}", task);
        
        if (task.CreatorId != task.PerformerId)
        {
            _logger.Information("Sending a request to create a notification to the performer in the NotificationService");

            var notificationId = await CreateNotificationAsync(task, notificationText, cancellationToken);
            
            _logger.Information("Notification created with id = {@notificationId}", notificationId);
        }
    }

    public async System.Threading.Tasks.Task AssignPerformerAsync(
        Guid taskId,
        Guid userId,
        CancellationToken cancellationToken
    )
    {
        const string notificationText = "Вас назначили исполнителем задачи";
        var task = await GetTaskAsync(taskId, cancellationToken);
        await task.AssignPerformerAsync(userId, _unitOfWork, cancellationToken);
        _logger.Information("Assigned a performer for the task: {@task}", task);

        if (task.CreatorId != userId)
        {
            _logger.Information("I am sending a request to create a notification to the performer in the NotificationService");

            var notificationId = await CreateNotificationAsync(task, notificationText, cancellationToken);
            
            _logger.Information("Notification created with id = {@notificationId}", notificationId);
        }
    }

    public async Task<PagedList<Task>> GetTaskListAsync(PageQueryFilter filter, CancellationToken cancellationToken)
    {
        var taskList = await _unitOfWork.Tasks.GetAllAsync(cancellationToken);

        if (filter.Id != null)
            taskList = taskList.Where(task => task.Id == filter.Id);

        if (filter.CreatorId != null)
            taskList = taskList.Where(task => task.CreatorId == filter.CreatorId);
        
        if (filter.PerformerId != null)
            taskList = taskList.Where(task => task.PerformerId == filter.PerformerId);

        if (!string.IsNullOrEmpty(filter.Title))
            taskList = taskList.Where(task => task.Title.Contains(filter.Title, StringComparison.CurrentCultureIgnoreCase));

        if (!string.IsNullOrEmpty(filter.Description))
            taskList = taskList.Where(task =>
                    task.Description != null &&
                    task.Description.Contains(filter.Description, StringComparison.CurrentCultureIgnoreCase));
        
        var result = taskList.OrderBy(task => task.Id)
            .ToArray();
        
        return new PagedList<Task>(result, filter.Page, filter.PageSize, result.Length);
    }

    private async Task<Guid> CreateNotificationAsync(
        Task task,
        string text,
        CancellationToken cancellationToken)
    {
        var notificationRequest = new NotificationRequest
        {
            UserId = task.PerformerId,
            Text = text,
            TaskId = task.Id,
        };
            
        var notificationId = await _notificationService.CreateNotificationAsync(
            notificationRequest,
            cancellationToken);

        return notificationId;
    }
}