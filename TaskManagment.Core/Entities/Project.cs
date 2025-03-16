namespace TaskManagment.Core.Entities;

public class Project: BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public Guid OwnerId { get; set; }
    public User Owner { get; set; } = null!;
    public List<ProjectMember> Members { get; set; } = new List<ProjectMember>();
    public List<Tasks> Tasks { get; set; } = new List<Tasks>();
}
