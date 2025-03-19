using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskManagment.Core.Entities;
using TaskManagment.Core.Enums;

namespace TaskManagment.Infrastructure.Data.Configurations;

public class ProjectMemberConfiguration: BaseEntityConfiguration<ProjectMember>
{
    public override void Configure(EntityTypeBuilder<ProjectMember> builder)
    {
        base.Configure(builder);

        builder.ToTable("ProjectMember");

        builder.Property(pm => pm.UserId)
            .IsRequired();

        builder.Property(pm => pm.Role)
            .IsRequired()
            .HasConversion<string>();

        builder.HasOne(pm => pm.User)
            .WithMany()
            .HasForeignKey(pm => pm.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(pm => pm.Project)
            .WithMany(p => p.Members)
            .HasForeignKey(pm => pm.ProjectId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}