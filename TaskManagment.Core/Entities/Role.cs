namespace TaskManagment.Core.Entities;

/// <summary>
/// Предоставляет системную роль пользователя
/// Определяет глобальные права
/// </summary>
public class Role: BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public List<UserRole> Roles { get; set; } = new List<UserRole>();
}
