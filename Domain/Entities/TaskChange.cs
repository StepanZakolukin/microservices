using System.Text.Json;
using Domain.Entities.Base;

namespace Domain.Entities;

public class TaskChange : BaseEntity<Guid>
{
    public Guid TaskId { get; private set; }
    
    public DateTime Date { get; private set; }
    
    public string TaskState { get; private set; }
    
    public Task Task { get; private set; }

    internal TaskChange()
    {
    }
    
    public TaskChange(Task task)
    {
        Id = Guid.NewGuid();
        Date = DateTime.UtcNow;
        TaskState = JsonSerializer.Serialize(task);
        TaskId = task.Id;
        Task = task;
    }
}