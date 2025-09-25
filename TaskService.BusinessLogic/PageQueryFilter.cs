namespace TaskService.BusinessLogic;

public record PageQueryFilter
{
    public Guid? Id { get; init; }
    
    public Guid? PerformerId { get; init; }
    
    public Guid? CreatorId { get; init; }
    
    public string? Title { get; init; }
    
    public string? Description { get; init; }
    
    public required int Page { get; init; }
    
    public required int PageSize { get; init; }
}