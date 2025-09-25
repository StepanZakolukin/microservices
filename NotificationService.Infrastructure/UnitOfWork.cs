using NotificationService.Infrastructure.Interfaces;

namespace NotificationService.Infrastructure;

internal class UnitOfWork : IUnitOfWork
{
    public INotificationRepository Notifications { get; init; }
    
    public UnitOfWork(AppDbContext dbContext, INotificationRepository notifications)
    {
        _dbContext = dbContext;
        Notifications = notifications;
    }
    
    private readonly AppDbContext _dbContext;

    
    public async Task SaveChangesAsync(CancellationToken cancellationToken)
    {
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}