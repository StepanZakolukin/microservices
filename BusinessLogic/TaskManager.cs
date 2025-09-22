using BusinessLogic.Interfaces;
using Domain.Interfaces;
using Task = Domain.Entities.Task;

namespace BusinessLogic;

public class TaskManager : ITaskManager
{
    private readonly ITaskRepository _taskRepository;
    
    public TaskManager(ITaskRepository taskRepository)
    {
        _taskRepository = taskRepository;
    }
    
    public async Task<Guid> CreateTaskAsync(
        string title,
        string? description,
        CancellationToken cancellationToken)
    {
        var task = Task.Create(title, description);
        await _taskRepository.AddAsync(task, cancellationToken);
        return task.Id;
    }

    public async System.Threading.Tasks.Task DeleteTaskAsync(Guid id, CancellationToken cancellationToken)
    {
        var task = await GetTaskAsync(id, cancellationToken);
        await task.DeleteAsync(_taskRepository, cancellationToken);
    }

    public async Task<Task> GetTaskAsync(Guid id, CancellationToken cancellationToken)
    {
        var task = await _taskRepository.GetByIdAsync(id, cancellationToken);
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
        await task.UpdateAsync(title, description, _taskRepository, cancellationToken);
    }

    public async System.Threading.Tasks.Task AssignPerformerAsync(
        Guid taskId,
        Guid userId,
        CancellationToken cancellationToken
    )
    {
        var task = await GetTaskAsync(taskId, cancellationToken);
        await task.AssignPerformerAsync(userId, _taskRepository, cancellationToken);
    }
}