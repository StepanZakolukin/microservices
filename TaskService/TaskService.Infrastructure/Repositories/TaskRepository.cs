using TaskService.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace TaskService.Infrastructure.Repositories;

internal class TaskRepository : ITaskRepository
{
    private readonly AppDbContext _dbContext;
    
    public TaskRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task AddAsync(Domain.Entities.Task task, CancellationToken cancellationToken)
    {
        await _dbContext.Tasks.AddAsync(task, cancellationToken);
    }

    public void Update(Domain.Entities.Task task)
    {
        _dbContext.Tasks.Update(task);
    }

    public async Task<Domain.Entities.Task> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _dbContext.Tasks
            .Include(task => task.Changes)
            .FirstAsync(task => task.Id == id, cancellationToken: cancellationToken);
    }

    public async Task<IEnumerable<Domain.Entities.Task>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _dbContext.Tasks.ToArrayAsync(cancellationToken);
    }
}