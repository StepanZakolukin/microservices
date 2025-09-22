namespace Domain.Interfaces;

public interface ITaskRepository
{
    Task AddAsync(Entities.Task task, CancellationToken cancellationToken);
    
    Task UpdateAsync(Entities.Task task, CancellationToken cancellationToken);
    
    Task<Entities.Task> GetByIdAsync(Guid id, CancellationToken cancellationToken);
}