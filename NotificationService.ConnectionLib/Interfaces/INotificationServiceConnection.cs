namespace NotificationService.ConnectionLib.Interfaces;

public interface INotificationServiceConnection
{
    Task CreateNotificationAsync(NotificationInfo notificationInfo, CancellationToken cancellationToken);
}