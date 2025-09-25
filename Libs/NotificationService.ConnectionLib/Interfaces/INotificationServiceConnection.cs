namespace NotificationService.ConnectionLib.Interfaces;

public interface INotificationServiceConnection
{
    Task<Guid> CreateNotificationAsync(NotificationRequest notificationRequest, CancellationToken cancellationToken);
}