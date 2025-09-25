namespace TaskService.Domain.Interfaces;

public interface IUnitOfWork
{
    IChangeRepository Changes { get; }
    
    ITaskRepository Tasks { get; }
    
    public Task SaveAsync(CancellationToken cancellationToken);
}