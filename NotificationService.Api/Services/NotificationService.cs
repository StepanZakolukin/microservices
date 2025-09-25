using Microsoft.AspNetCore.SignalR;
using NotificationService.Api.Hubs;
using NotificationService.Api.Services.Interfaces;

namespace NotificationService.Api.Services;

internal class NotificationService : INotificationService
{
    private const string Method = "SendNotification";
    
    private readonly IHubContext<NotificationHub> _hubContext;

    public NotificationService(IHubContext<NotificationHub> hubContext)
    {
        _hubContext = hubContext;
    }

    public async Task SendNotificationAsync(string userId, string message, CancellationToken cancellationToken)
    {
        await _hubContext.Clients.Group(userId).SendAsync(Method, message, cancellationToken);
    }
}