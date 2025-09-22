using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.ModelConfigurations;

internal class TaskChangeConfiguration : IEntityTypeConfiguration<TaskChange>
{
    public void Configure(EntityTypeBuilder<TaskChange> builder)
    {
        builder.HasKey(change => change.Id);
        
        builder
            .HasOne(change => change.Task)
            .WithMany(task => task.Changes)
            .HasForeignKey(change => change.TaskId);
        
        builder
            .Property(change => change.TaskState)
            .HasColumnType("jsonb");
    }
}