namespace Api.Controllers.Task.Response;

public record TaskResponse
{
    public required Guid Id { get; init; }
    
    public required Guid PerformerId { get; init; }
    
    public required Guid CreatorId { get; init; }
    
    public required string Title { get; init; }
    
    public required string? Description { get; init; }
}