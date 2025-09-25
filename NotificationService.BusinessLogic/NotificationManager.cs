using NotificationService.BusinessLogic.Interfaces;
using NotificationService.Domain.Entities;
using NotificationService.Infrastructure.Interfaces;

namespace NotificationService.BusinessLogic;

internal class NotificationManager : INotificationManager
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly Serilog.ILogger _logger;

    public NotificationManager(IUnitOfWork unitOfWork, Serilog.ILogger logger)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
    }

    public async Task<Guid> CreateNotificationAsync(Guid userId, Guid taskId, string message, CancellationToken cancellationToken)
    {
        var notification = new Notification
        {
            UserId = userId,
            Created = DateTime.UtcNow,
            Message = message,
            TaskId = taskId
        };
        
        await _unitOfWork.Notifications.AddNotificationAsync(notification, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        _logger.Information("Created notification {@notification}", notification);
        
        return notification.Id;
    }

    public async Task MarkAsReadAsync(Guid notificationId, CancellationToken cancellationToken)
    {
        var notification = await _unitOfWork.Notifications.GetNotificationAsync(notificationId, cancellationToken);
        notification.MarkAsRead();
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        _logger.Information("Notification with Id = {@notificationId} marked as read", notificationId);
    }

    public async Task<IEnumerable<Notification>> GetNotificationListAsync(Guid userId, CancellationToken cancellationToken)
    {
        var result = await _unitOfWork.Notifications.GetNotificationListAsync(userId, cancellationToken);
        return result;
    }
}