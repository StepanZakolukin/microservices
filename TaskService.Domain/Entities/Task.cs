using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using System.Text.Json.Serialization;
using TaskService.Domain.Entities.Base;
using TaskService.Domain.Interfaces;

namespace TaskService.Domain.Entities;

public partial class Task : BaseEntity<Guid>
{
    public bool Deleted { get; private set; }
    
    public Guid PerformerId { get; private set; }
    
    public string Title { get; private set; }
    
    public string? Description { get; private set; }

    [JsonIgnore]
    public IEnumerable<TaskChange> Changes => _changes.Select(change => change);

    private readonly List<TaskChange> _changes;

    private Task(string title, string? description = null, Guid id = default)
    {
        Title = title;
        Deleted = false;
        Description = description;
        Id = id == Guid.Empty ? Guid.NewGuid() : id;
        _changes = [ new TaskChange(this) ];
    }
}

public partial class Task
{
    public static Task Create(string title, string? description = null, Guid id = default)
    {
        return new Task(title, description, id);
    }
    
    public void ThrowExceptionIfDeleted()
    {
        if (Deleted)
            throw new InvalidOperationException("Task is already deleted");
    }
    
    public async System.Threading.Tasks.Task UpdateAsync(
        string title,
        string? description,
        ITaskRepository taskRepository,
        CancellationToken cancellationToken
    )
    {
        Title = title;
        Description = description;
        ThrowExceptionIfDeleted();
        
        var change = new TaskChange(this);
        _changes.Add(change);

        try
        {
            await taskRepository.UpdateAsync(this, cancellationToken);
        }
        catch
        {
            _changes.RemoveAt(_changes.Count - 1);
            var previousState = JsonSerializer.Deserialize<Task>(_changes[^1].TaskState);
            Title = previousState.Title;
            Description = previousState.Description;
            throw;
        }
    }
    
    public async System.Threading.Tasks.Task DeleteAsync(
        ITaskRepository taskRepository,
        CancellationToken cancellationToken
    )
    {
        ThrowExceptionIfDeleted();
        
        Deleted = true;
        var change = new TaskChange(this);
        _changes.Add(change);

        try
        {
            await taskRepository.UpdateAsync(this, cancellationToken);
        }
        catch
        {
            Deleted = false;
            _changes.RemoveAt(_changes.Count - 1);
            throw;
        }
    }

    public async System.Threading.Tasks.Task AssignPerformerAsync(
        Guid userId,
        ITaskRepository taskRepository,
        CancellationToken cancellationToken
    )
    {
        ThrowExceptionIfDeleted();

        PerformerId = userId;
        var change = new TaskChange(this);
        _changes.Add(change);

        try
        {
            await taskRepository.UpdateAsync(this, cancellationToken);
        }
        catch
        {
            _changes.RemoveAt(_changes.Count - 1);
            var previousState = JsonSerializer.Deserialize<Task>(_changes[^1].TaskState);
            PerformerId = previousState.PerformerId;
            throw;
        }
    }
}