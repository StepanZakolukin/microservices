using System.Text.Json.Serialization;
using Core.Domain.Entities.Base;
using Destructurama.Attributed;
using Domain.Interfaces;

namespace Domain.Entities;

public partial class Task : BaseEntity<Guid>
{
    public bool Deleted { get; private set; }
    
    public Guid PerformerId { get; private set; }
    
    public string Title { get; private set; }
    
    [LogAsScalar]
    public string? Description { get; private set; }

    [NotLogged]
    [JsonIgnore]
    public IEnumerable<TaskChange> Changes => _changes.AsEnumerable();

    private readonly List<TaskChange> _changes;

    internal Task()
    {
    }

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
        IUnitOfWork unitOfWork,
        CancellationToken cancellationToken
    )
    {
        ThrowExceptionIfDeleted();
        
        Title = title;
        Description = description;
        
        var change = new TaskChange(this);
        _changes.Add(change);

        await UpdateDataAsync(change, unitOfWork, cancellationToken);
    }
    
    public async System.Threading.Tasks.Task DeleteAsync(
        IUnitOfWork unitOfWork,
        CancellationToken cancellationToken
    )
    {
        ThrowExceptionIfDeleted();
        
        Deleted = true;
        var change = new TaskChange(this);
        _changes.Add(change);

        await UpdateDataAsync(change, unitOfWork, cancellationToken);
    }

    public async System.Threading.Tasks.Task AssignPerformerAsync(
        Guid userId,
        IUnitOfWork unitOfWork,
        CancellationToken cancellationToken
    )
    {
        ThrowExceptionIfDeleted();

        PerformerId = userId;
        var change = new TaskChange(this);
        _changes.Add(change);

        await UpdateDataAsync(change, unitOfWork, cancellationToken);
    }

    public async System.Threading.Tasks.Task UpdateDataAsync(
        TaskChange change,
        IUnitOfWork unitOfWork,
        CancellationToken cancellationToken
    )
    {
        await unitOfWork.Changes.AddAsync(change, cancellationToken);
        unitOfWork.Tasks.Update(this);
        await unitOfWork.SaveAsync(cancellationToken);
    }
}