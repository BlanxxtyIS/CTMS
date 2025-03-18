namespace TaskManagment.Core.Entities;


/// <summary>
/// Представляет комментарий к задаче
/// Используется для обсуждения задач и ведения истории коммуникации
/// </summary>
public class TaskComment : BaseEntity
{
    public string Content { get; set; } = string.Empty;

    public Guid TaskId { get; set; }
    public Task Task { get; set; } = null!;

    public Guid AuthorId { get; set; }
    public User Author { get; set; } = null!;
}
