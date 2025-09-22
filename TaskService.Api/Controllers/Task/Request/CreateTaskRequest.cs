namespace TaskService.Api.Controllers.Task.Request;

public record CreateTaskRequest
{
    public required string Title { get; init; }
    
    public string? Description { get; init; }
}