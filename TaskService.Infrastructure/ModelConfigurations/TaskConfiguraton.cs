using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Task = TaskService.Domain.Entities.Task;

namespace TaskService.Infrastructure.ModelConfigurations;

internal class TaskConfiguration : IEntityTypeConfiguration<Task>
{
    public void Configure(EntityTypeBuilder<Task> builder)
    {
        builder.HasKey(task => task.Id);
        
        builder.Property(task => task.Title)
            .HasColumnType("varchar(255)");
        
        builder.Property(task => task.Description)
            .HasColumnType("text");
    }
}