using System.ComponentModel.DataAnnotations;

namespace Api.Controllers.Task.Request;

public record TaskRequest
{
    [MaxLength(255, ErrorMessage = "The field Title must be 255 characters or less.")]
    public required string Title { get; init; }
    
    public string? Description { get; init; }
}