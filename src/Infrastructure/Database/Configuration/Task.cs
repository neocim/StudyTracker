using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Entity = Domain.Entities;

namespace Infrastructure.Database.Configuration;

public class TaskConfiguration : IEntityTypeConfiguration<Entity.Task>
{
    public void Configure(EntityTypeBuilder<Entity.Task> builder)
    {
        builder.HasKey(task => task.Id);

        builder.Property(task => task.OwnerId).IsRequired();
        builder.HasIndex(task => task.OwnerId);

        builder.HasOne(task => task.Parent)
            .WithMany(task => task.SubTasks)
            .HasForeignKey(task => task.ParentId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired(false);

        builder.Property(task => task.Name);
        builder.Property(task => task.Description).HasDefaultValue(null);
        builder.Property(task => task.Success).HasDefaultValue(null);
        builder.Property(task => task.BeginDate);
        builder.Property(task => task.EndDate);
    }
}