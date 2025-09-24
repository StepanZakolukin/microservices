namespace NotificationService.ConnectionLib;

public record NotificationInfo
{
    public required string Message { get; init; }
    
    public required string UserId { get; init; }
}