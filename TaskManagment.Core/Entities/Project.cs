namespace TaskManagment.Core.Entities;

/// <summary>
/// Предоставляет проект в системе управления задачами.
/// Проект содержит набор задач и участников с различными ролями
/// </summary>
public class Project: BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    /// <summary>
    /// Идентификатор владельца проекта
    /// </summary>
    public Guid OwnerId { get; set; }
    public User Owner { get; set; } = null!;
    public List<ProjectMember> Members { get; set; } = new List<ProjectMember>();
    public List<WorkTask > Tasks { get; set; } = new List<WorkTask >();
}
