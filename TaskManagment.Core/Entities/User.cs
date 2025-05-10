namespace TaskManagment.Core.Entities;


/// <summary>
/// Представляет пользователя системы
/// Содержит информацию о профиле, аутентификации, ролях пользователя, его задачах, комментариях
/// </summary>
public class User: BaseEntity
{
    public string Username { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public bool IsActive { get; set; }
    public List<UserRole> UserRoles { get; set; } = new List<UserRole>();
    public List<Project> OwnedProjects { get; set; } = new List<Project>();
    public List<WorkTask> AssignedTasks { get; set; } = new List<WorkTask >();
    public List<TaskComment> Comments { get; set; } = new List<TaskComment>();
}
