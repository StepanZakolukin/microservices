namespace NotificationService.Api.Controllers.Notification.Response;

public class NotificationResponse
{
    public required Guid Id { get; init; }
    
    public required DateTime Created { get; init; }
    
    public required string Message { get; init; }
    
    public required Guid TaskId { get; init; }

    public required bool ReadIt { get; init; }
}