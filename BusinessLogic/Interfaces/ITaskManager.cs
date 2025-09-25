using Saritasa.Tools.Common.Pagination;

namespace BusinessLogic.Interfaces;

public interface ITaskManager
{
    Task<Guid> CreateTaskAsync(string title, string? description, Guid creatorId, CancellationToken cancellationToken);
    
    Task DeleteTaskAsync(Guid id, CancellationToken cancellationToken);
    
    Task<Domain.Entities.Task> GetTaskAsync(Guid id, CancellationToken cancellationToken);
    
    Task UpdateTaskAsync(Guid id, string title, string? description, CancellationToken cancellationToken);
    
    Task AssignPerformerAsync(Guid taskId, Guid userId, CancellationToken cancellationToken);
    
    Task<PagedList<Domain.Entities.Task>> GetTaskListAsync(PageQueryFilter filter, CancellationToken cancellationToken);
}