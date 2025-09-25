namespace Api.Controllers.Task.Request;

public record CreateTaskRequest
{
    public required Guid CreatorId  { get; init; }
    
    public required string Title { get; init; }
    
    public string? Description { get; init; }
}