using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Task = Domain.Entities.Task;

namespace Infrastructure.Database.Configuration;

public class TaskConfiguration : IEntityTypeConfiguration<Task>
{
    public void Configure(EntityTypeBuilder<Task> builder)
    {
        builder.HasKey(task => task.Id);

        builder
            .HasMany(task => task.SubTasks).WithOne()
            .HasForeignKey("ParentTaskId")
            .IsRequired(false)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Property(task => task.Name);
        builder.Property(task => task.Description).HasDefaultValue(null);
        builder.Property(task => task.Success).HasDefaultValue(null);
        builder.Property(task => task.BeginDate);
        builder.Property(task => task.EndDate);
    }
}