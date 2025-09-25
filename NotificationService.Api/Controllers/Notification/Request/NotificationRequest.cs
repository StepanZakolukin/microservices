namespace NotificationService.Api.Controllers.Notification.Request;

public record NotificationRequest
{
    public required Guid UserId { get; init; }
    
    public required string Text { get; init; }
    
    public required Guid TaskId { get; init; }
}