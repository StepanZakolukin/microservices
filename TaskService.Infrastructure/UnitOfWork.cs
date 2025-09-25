using TaskService.Domain.Interfaces;

namespace TaskService.Infrastructure.Repositories;

internal class UnitOfWork : IUnitOfWork
{
    public IChangeRepository Changes { get; init; }
    
    public ITaskRepository Tasks { get; init; }
    
    private readonly AppDbContext _dbContext;
    
    public UnitOfWork(AppDbContext dbContext, IChangeRepository changes, ITaskRepository tasks)
    {
        _dbContext = dbContext;
        Tasks = tasks;
        Changes = changes;
    }
    
    public async Task SaveAsync(CancellationToken cancellationToken)
    {
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}