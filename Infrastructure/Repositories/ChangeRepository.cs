using Domain.Entities;
using Domain.Interfaces;
using Task = System.Threading.Tasks.Task;

namespace Infrastructure.Repositories;

internal class ChangeRepository : IChangeRepository
{
    private readonly AppDbContext _dbContext;
    
    public ChangeRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task AddAsync(TaskChange taskChange, CancellationToken cancellationToken)
    {
        await _dbContext.Changes.AddAsync(taskChange, cancellationToken);
    }
}