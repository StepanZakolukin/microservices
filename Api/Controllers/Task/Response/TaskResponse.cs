namespace Api.Controllers.Task.Response;

public record TaskResponse
{
    public required Guid Id { get; init; }
    
    public required Guid PerformerId { get; init; }
    
    public required string Title { get; init; }
    
    public string? Description { get; init; }
}