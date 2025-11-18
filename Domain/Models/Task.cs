using Domain.Enums;

namespace Domain.Models; 

public class Task : BaseModel
{
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? DeadLine { get; set; }
    public ETaskPriority Priority { get; set; }
    public Guid ProjectId { get; set; }
    public Project Project { get; set; }

    public ICollection<Task> SubTasks { get; set; }
    public ICollection<Attach> Attaches { get; set; }
    public ICollection<User> AssignedTo { get; set; }
    public ICollection<Tag> Tags { get; set; }
}
