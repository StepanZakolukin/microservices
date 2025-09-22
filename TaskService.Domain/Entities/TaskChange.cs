using System.Text.Json;
using TaskService.Domain.Entities.Base;

namespace TaskService.Domain.Entities;

public class TaskChange : BaseEntity<Guid>
{
    public Guid TaskId { get; private set; }
    
    public DateTime Date { get; private set; }
    
    public string TaskState { get; private set; }
    
    public Task Task { get; private set; }

    protected TaskChange()
    {
    }
    
    public TaskChange(Task task)
    {
        Id = Guid.NewGuid();
        Date = DateTime.Now;
        TaskState = JsonSerializer.Serialize(task);
        TaskId = task.Id;
        Task = task;
    }
}