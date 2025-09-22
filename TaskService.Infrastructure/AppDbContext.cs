using Microsoft.EntityFrameworkCore;
using TaskService.Domain.Entities;
using Task = TaskService.Domain.Entities.Task;

namespace TaskService.Infrastructure;

/// <summary>
///     Application unit of work.
/// </summary>
internal class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Task> Tasks => Set<Task>();
    
    public DbSet<TaskChange> Changes => Set<TaskChange>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }
}
