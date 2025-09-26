namespace TaskService.Api.Controllers.Task.Request;

public record UserRequest
{
    public required Guid PerformerId { get; init; }
}