using NotificationService.Domain.Entities;

namespace NotificationService.BusinessLogic.Interfaces;

public interface INotificationManager
{
    Task<Guid> CreateNotificationAsync(Guid userId, Guid taskId, string message, CancellationToken cancellationToken);
    
    Task MarkAsReadAsync(Guid notificationId, CancellationToken cancellationToken);
    
    Task<IEnumerable<Notification>> GetNotificationListAsync(Guid userId, CancellationToken cancellationToken);
}