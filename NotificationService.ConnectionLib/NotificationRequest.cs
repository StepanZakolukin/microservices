namespace NotificationService.ConnectionLib;

public record NotificationRequest
{
    public required Guid UserId { get; init; }
    
    public required string Message { get; init; }
    
    public required Guid TaskId { get; init; }
}