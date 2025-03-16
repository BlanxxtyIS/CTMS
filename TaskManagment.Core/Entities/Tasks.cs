using TaskManagment.Core.Enums;
namespace TaskManagment.Core.Entities;

public class Tasks : BaseEntity
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public TaskPriority Priority { get; set; } = TaskPriority.Medium;
    public TaskStatusEnum Status { get; set; } = TaskStatusEnum.ToDo;
    public DateTime? DueDate { get; set; }

    public Guid ProjectId { get; set; }
    public Project Project { get; set; } = null!;

    public Guid? AssigneeId { get; set; }
    public User? Assignee { get; set; }

    public List<TaskComment> Comments { get; set; } = new List<TaskComment>();
}



