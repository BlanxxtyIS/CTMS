using TaskManagment.Core.Enums;

namespace TaskManagment.Core.Entities;

/// <summary>
/// Предоставляет участника проекта с определенной ролью
/// Связывает пользователя с проектом и определяет роль
/// </summary>
public class ProjectMember: BaseEntity
{
    public Guid UserId { get; set; }
    public User User { get; set; } = null!;
    public Guid ProjectId { get; set; }
    public Project Project { get; set; } = null!;
    public ProjectRole Role { get; set; }
}


