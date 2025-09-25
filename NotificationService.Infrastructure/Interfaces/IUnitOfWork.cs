namespace NotificationService.Infrastructure.Interfaces;

public interface IUnitOfWork
{
    INotificationRepository Notifications { get; }
    
    Task SaveChangesAsync(CancellationToken cancellationToken);
}