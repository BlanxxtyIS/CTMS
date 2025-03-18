using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskManagment.Core.Entities;

namespace TaskManagment.Infrastructure.Data.Configurations;

public class TaskCommentConfiguration: BaseEntityConfiguration<TaskComment>
{
    public override void Configure(EntityTypeBuilder<TaskComment> builder)
    {
        base.Configure(builder);

        builder.ToTable("TaskComments");

        builder.Property(tc => tc.Content)
                .IsRequired()
                .HasMaxLength(1000);

        builder.Property(tc => tc.TaskId)
            .IsRequired();

        builder.Property(tc => tc.AuthorId)
            .IsRequired();

        builder.HasOne(tc => tc.Task)
            .WithMany(t => t.Comments)
            .HasForeignKey(tc => tc.TaskId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(tc => tc.Author)
            .WithMany(u => u.Comments)
            .HasForeignKey(tc => tc.AuthorId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
