namespace TaskService.Api.Controllers.Task.Responce;

public record TaskResponse
{
    public required Guid Id { get; init; }
    
    public required Guid PerformerId { get; init; }
    
    public required string Title { get; init; }
    
    public string? Description { get; init; }
}