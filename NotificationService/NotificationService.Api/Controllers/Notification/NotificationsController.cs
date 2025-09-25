using Microsoft.AspNetCore.Mvc;
using NotificationService.Api.Controllers.Notification.Request;
using NotificationService.Api.Controllers.Notification.Response;
using NotificationService.Api.Services.Interfaces;
using NotificationService.BusinessLogic.Interfaces;

namespace NotificationService.Api.Controllers.Notification;

[ApiController]
[Route("api/notifications")]
public class NotificationsController : ControllerBase
{
    private readonly INotificationManager _notificationManager;
    private readonly INotificationService _notificationService;
    public NotificationsController(INotificationManager notificationManager, INotificationService notificationService)
    {
        _notificationManager = notificationManager;
        _notificationService = notificationService;
    }

    [HttpGet("{userId:guid}")]
    [ProducesResponseType<IEnumerable<NotificationResponse>>(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetNotificationListAsync([FromRoute] Guid userId, CancellationToken cancellationToken)
    {
        var result = (await _notificationManager.GetNotificationListAsync(userId, cancellationToken))
            .Select(notification => new NotificationResponse
            {
                Created = notification.Created,
                Text = notification.Text,
                TaskId = notification.TaskId,
                ReadIt = notification.ReadIt,
                Id = notification.Id,
            });
        
        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType<Guid>(StatusCodes.Status201Created)]
    public async Task<IActionResult> CreateNotificationAsync(
        [FromBody] NotificationRequest notification,
        CancellationToken cancellationToken)
    {
        var result = await _notificationManager.CreateNotificationAsync(
            notification.UserId,
            notification.TaskId,
            notification.Text,
            cancellationToken);
        
        await _notificationService.SendNotificationAsync(
            $"{notification.UserId}",
            notification.Text,
            cancellationToken);

        return Created("api/notifications", result);
    }
    
    [HttpPut("{id:guid}/mark-as-read")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> MarkAsRead([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        await _notificationManager.MarkAsReadAsync(id, cancellationToken);
        return NoContent();
    }
}