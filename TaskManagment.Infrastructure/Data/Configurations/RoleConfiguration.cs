using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskManagment.Core.Entities;

namespace TaskManagment.Infrastructure.Data.Configurations;

public class RoleConfiguration: BaseEntityConfiguration<Role>
{
    public override void Configure(EntityTypeBuilder<Role> builder)
    {
     
        base.Configure(builder);

        builder.Property(r => r.Name)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(r => r.Description)
            .HasMaxLength(200);

        builder.HasIndex(r => r.Name)
            .IsUnique();

        //Одна роль со
        builder.HasMany(r => r.Roles)
            .WithOne(u => u.Role)
            .HasForeignKey(u => u.RoleId)
            .OnDelete(DeleteBehavior.Cascade);

        SeedRoles(builder);
    }

    private void SeedRoles(EntityTypeBuilder<Role> builder)
    {
        var adminRoleId = Guid.Parse("8D04DCE2-969A-435D-BBA4-DF3F325983DC");
        var managerRoleId = Guid.Parse("3E52D2BA-8811-41A9-88F1-87C312FBD1F1");
        var userRoleId = Guid.Parse("F94B632F-A86C-479A-AE91-7F3CD0557704");

        builder.HasData(
            new Role
            {
                Id = adminRoleId,
                Name = "Administrator",
                Description = "Полный доступ к системе",
                CreatedAt = DateTime.UtcNow
            },
            new Role
            {
                Id = managerRoleId,
                Name = "Manager",
                Description = "Управляет пользователями и проектами",
                CreatedAt = DateTime.UtcNow
            },
            new Role
            {
                Id = userRoleId,
                Name = "User",
                Description = "Юзер с ограниченными возможностями",
                CreatedAt = DateTime.UtcNow
            }
        );
    }
}