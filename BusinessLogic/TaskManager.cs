using BusinessLogic.Interfaces;
using Domain.Interfaces;
using Task = Domain.Entities.Task;

namespace BusinessLogic;

public class TaskManager : ITaskManager
{
    private readonly Serilog.ILogger _logger;
    private readonly IUnitOfWork _unitOfWork;
    
    public TaskManager(IUnitOfWork unitOfWork, Serilog.ILogger logger)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
    }
    
    public async Task<Guid> CreateTaskAsync(
        string title,
        string? description,
        CancellationToken cancellationToken)
    {
        var task = Task.Create(title, description);
        await _unitOfWork.Tasks.AddAsync(task, cancellationToken);
        return task.Id;
    }

    public async System.Threading.Tasks.Task DeleteTaskAsync(Guid id, CancellationToken cancellationToken)
    {
        var task = await GetTaskAsync(id, cancellationToken);
        await task.DeleteAsync(_unitOfWork, cancellationToken);
    }

    public async Task<Task> GetTaskAsync(Guid id, CancellationToken cancellationToken)
    {
        _logger.Information("Отправил запрос на получение {TaskName} с id = {Guid}", nameof(Task), id);
        var task = await _unitOfWork.Tasks.GetByIdAsync(id, cancellationToken);
        _logger.Information("Получил {TaskName} с id = {Guid}", nameof(Task), id);
        return task;
    }

    public async System.Threading.Tasks.Task UpdateTaskAsync(
        Guid id,
        string title,
        string? description,
        CancellationToken cancellationToken
    )
    {
        var task = await GetTaskAsync(id, cancellationToken);
        await task.UpdateAsync(title, description, _unitOfWork, cancellationToken);
    }

    public async System.Threading.Tasks.Task AssignPerformerAsync(
        Guid taskId,
        Guid userId,
        CancellationToken cancellationToken
    )
    {
        var task = await GetTaskAsync(taskId, cancellationToken);
        await task.AssignPerformerAsync(userId, _unitOfWork, cancellationToken);
    }
}