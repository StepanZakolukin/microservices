using Domain.Entities;
using Task = System.Threading.Tasks.Task;

namespace Domain.Interfaces;

public interface IChangeRepository
{
    Task AddAsync(TaskChange taskChange, CancellationToken cancellationToken);
}