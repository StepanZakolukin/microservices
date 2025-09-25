namespace TaskService.Domain.Interfaces;

public interface ITaskRepository
{
    Task AddAsync(Entities.Task task, CancellationToken cancellationToken);
    
    void Update(Entities.Task task);
    
    Task<Entities.Task> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    
    Task<IEnumerable<Entities.Task>> GetAllAsync(CancellationToken cancellationToken);
}