using Microsoft.AspNetCore.Mvc;

namespace NotificationService.Api.Controllers;

[ApiController]
[Route("api/notifications")]
public class NotificationsController : ControllerBase
{
    public NotificationsController()
    {
    }

    [HttpGet("{userId:guid}")]
    public async Task<IActionResult> GetNotificationsAsync([FromRoute] Guid userId)
    {
        throw new NotImplementedException();
    }

    [HttpPost]
    public async Task<IActionResult> CreateNotification()
    {
        throw new NotImplementedException();
    }
    
    [HttpPut("{id:guid}/mark-as-read")]
    public async Task<IActionResult> MarkAsRead([FromRoute] Guid id)
    {
        throw new NotImplementedException();
    }
}