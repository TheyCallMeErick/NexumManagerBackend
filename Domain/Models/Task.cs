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

    public IEnumerable<Task> SubTasks { get; set; }
    public IEnumerable<Attach> Attaches { get; set; }
    public IEnumerable<UserOnProject> AssignedTo { get; set; }
    public IEnumerable<Tag> Tags { get; set; }
}
