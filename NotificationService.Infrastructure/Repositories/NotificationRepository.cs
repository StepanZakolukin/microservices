using Microsoft.EntityFrameworkCore;
using NotificationService.Domain.Entities;
using NotificationService.Infrastructure.Interfaces;

namespace NotificationService.Infrastructure.Repositories;

internal class NotificationRepository : INotificationRepository
{
    private readonly AppDbContext _dbContext;

    public NotificationRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void Update(Notification notification)
    {
        _dbContext.Notifications.Update(notification);
    }

    public async Task<IEnumerable<Notification>> GetNotificationListAsync(Guid userId, CancellationToken cancellationToken)
    {
        IQueryable<Notification> notifications = _dbContext.Notifications;
        return await notifications
            .Where(notification => notification.UserId == userId)
            .ToListAsync(cancellationToken: cancellationToken);
    }

    public async Task AddNotificationAsync(Notification notification, CancellationToken cancellationToken)
    {
        await _dbContext.Notifications.AddAsync(notification, cancellationToken);
    }

    public async Task<Notification> GetNotificationAsync(Guid notificationId, CancellationToken cancellationToken)
    {
        IQueryable<Notification> notifications = _dbContext.Notifications;
        return await notifications.FirstOrDefaultAsync(notification => notification.Id == notificationId)
               ?? throw new InvalidOperationException($"Notification with Id = {notificationId} not found");
    }
}