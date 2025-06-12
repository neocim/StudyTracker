using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Task = Domain.Entities.Task;

namespace Infrastructure.Database.Configuration;

public class TaskConfiguration : IEntityTypeConfiguration<Task>
{
    public void Configure(EntityTypeBuilder<Task> builder)
    {
        builder.HasKey(task => task.Id);

        builder.HasOne(task => task.Owner).WithMany(user => user.Tasks);

        builder.Property(task => task.Success);
        builder.Property(task => task.FromDate);
        builder.Property(task => task.ToDate);
    }
}