using Core.Domain.Entities.Base;
using Destructurama.Attributed;

namespace NotificationService.Domain.Entities;

public class Notification : BaseEntity<Guid>
{
    public required Guid UserId { get; init; }
    
    public required DateTime Created { get; init; }
    
    [LogAsScalar]
    public required string Text { get; init; }
    
    public required Guid TaskId { get; init; }

    public bool ReadIt { get; private set; } = false;

    public void MarkAsRead()
    {
        ReadIt = true;
    }

    public Notification()
    {
        Id = Guid.NewGuid();
    }
}