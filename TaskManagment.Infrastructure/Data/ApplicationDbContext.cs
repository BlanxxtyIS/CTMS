using Microsoft.EntityFrameworkCore;
using TaskManagment.Core.Entities;
using TaskManagment.Infrastructure.Data.Configurations;

namespace TaskManagment.Infrastructure.Data;

public class ApplicationDbContext: DbContext 
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base (options) { }

    public DbSet<User> Users { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<UserRole> UserRoles { get; set; }
    public DbSet<Project> Projects { get; set; }
    public DbSet<ProjectMember> ProjectMembers { get; set; }
    public DbSet<WorkTask> WorkTasks { get; set; }
    public DbSet<TaskComment> TaskComments { get; set; }
    public DbSet<RefreshToken> RefreshTokens { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UserConfiguration());
        modelBuilder.ApplyConfiguration(new RoleConfiguration());
        modelBuilder.ApplyConfiguration(new UserRoleConfigurattion());
        modelBuilder.ApplyConfiguration(new ProjectsConfiguration());
        modelBuilder.ApplyConfiguration(new ProjectMemberConfiguration());
        modelBuilder.ApplyConfiguration(new WorkTaskConfiguration());
        modelBuilder.ApplyConfiguration(new TaskCommentConfiguration());
    }
}
