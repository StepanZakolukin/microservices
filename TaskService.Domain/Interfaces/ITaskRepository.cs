namespace TaskService.Domain.Interfaces;

public interface ITaskRepository
{
    Task AddAsync(TaskService.Domain.Entities.Task task, CancellationToken cancellationToken);
    
    Task UpdateAsync(TaskService.Domain.Entities.Task task, CancellationToken cancellationToken);
    
    Task<TaskService.Domain.Entities.Task> GetByIdAsync(Guid id, CancellationToken cancellationToken);
}