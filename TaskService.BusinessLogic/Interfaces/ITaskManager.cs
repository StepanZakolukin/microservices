namespace TaskService.BusinessLogic.Interfaces;

public interface ITaskManager
{
    Task<Guid> CreateTaskAsync(string title, string? description, CancellationToken cancellationToken);
    
    Task DeleteTaskAsync(Guid id, CancellationToken cancellationToken);
    
    Task<TaskService.Domain.Entities.Task> GetTaskAsync(Guid id, CancellationToken cancellationToken);
    
    Task UpdateTaskAsync(Guid id, string title, string? description, CancellationToken cancellationToken);
    
    Task AssignPerformerAsync(Guid taskId, Guid userId, CancellationToken cancellationToken);
}