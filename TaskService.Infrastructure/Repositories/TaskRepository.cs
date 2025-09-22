using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

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
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Domain.Entities.Task task, CancellationToken cancellationToken)
    {
        _dbContext.Tasks.Update(task);
        var result = await _dbContext.SaveChangesAsync(cancellationToken);
        if (result == 0) throw new InvalidOperationException("The object was not found");
    }

    public async Task<Domain.Entities.Task> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _dbContext.Tasks
            .Include(task => task.Changes)
            .FirstAsync(t => t.Id == id, cancellationToken: cancellationToken);
    }
}