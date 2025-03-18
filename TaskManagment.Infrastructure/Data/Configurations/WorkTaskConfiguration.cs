using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskManagment.Core.Entities;
using TaskManagment.Core.Enums;

namespace TaskManagment.Infrastructure.Data.Configurations;

public class WorkTaskConfiguration: BaseEntityConfiguration<WorkTask>
{
    public override void Configure(EntityTypeBuilder<WorkTask> builder)
    {
        base.Configure(builder);

        builder.ToTable("WorkTasks");

        builder.Property(wt => wt.Title)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(wt => wt.Description)
            .HasMaxLength(1000);

        builder.Property(wt => wt.Priority)
            .IsRequired()
            .HasConversion<string>();

        builder.Property(wt => wt.Status)
            .IsRequired()
            .HasConversion<string>();

        builder.Property(wt => wt.Priority)
            .HasDefaultValue(TaskPriority.Low);

        builder.Property(wt => wt.Status)
            .HasDefaultValue(TaskStatus.Created);

        builder.Property(wt => wt.ProjectId)
            .IsRequired();

        builder.Property(t => t.AssigneeId)
            .IsRequired(false);

        builder.HasOne(t => t.Project)
            .WithMany(p => p.Tasks)
            .HasForeignKey(t => t.ProjectId)
            .OnDelete(DeleteBehavior.Cascade);


        builder.HasOne(t => t.Assignee)
            .WithMany(u => u.AssignedTasks)
            .HasForeignKey(t => t.AssigneeId)
            .OnDelete(DeleteBehavior.SetNull); 

        builder.HasMany(t => t.Comments)
            .WithOne(tc => tc.Task)
            .HasForeignKey(tc => tc.TaskId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}