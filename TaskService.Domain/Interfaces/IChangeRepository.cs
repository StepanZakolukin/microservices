using TaskService.Domain.Entities;
using Task = System.Threading.Tasks.Task;

namespace TaskService.Domain.Interfaces;

public interface IChangeRepository
{
    Task AddAsync(TaskChange taskChange, CancellationToken cancellationToken);
}