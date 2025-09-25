using NotificationService.Domain.Entities;

namespace NotificationService.Infrastructure.Interfaces;

public interface INotificationRepository
{
    void Update(Notification notification);

    Task<IEnumerable<Notification>> GetNotificationListAsync(Guid userId, CancellationToken cancellationToken);
    
    Task AddNotificationAsync(Notification notification, CancellationToken cancellationToken);

    Task<Notification> GetNotificationAsync(Guid notificationId, CancellationToken cancellationToken);
}