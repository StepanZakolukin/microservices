using Microsoft.AspNetCore.SignalR;
using NotificationService.Api.Services.Interfaces;
using NotificationService.BusinessLogic.Interfaces;

namespace NotificationService.Api.Hubs;

public class NotificationHub : Hub
{
    private readonly INotificationManager _notificationManager;
    private readonly INotificationService _notificationService;

    public NotificationHub(INotificationManager notificationManager, INotificationService notificationService)
    {
        _notificationManager = notificationManager;
        _notificationService = notificationService;
    }

    public override async Task OnConnectedAsync()
    {
        var userId = Context.GetHttpContext().Request.Query["userId"].FirstOrDefault();
        
        if (!string.IsNullOrEmpty(userId))
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, userId);

            var guidUserId = Guid.Parse(userId);
            var notificationList = await _notificationManager.GetUnreadNotificationListAsync(guidUserId);
            
            foreach (var notification in notificationList)
                await _notificationService.SendNotificationAsync(userId, notification);
        }
    }
}