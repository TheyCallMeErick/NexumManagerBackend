namespace Domain.Models; 

public class Task : BaseModel
{
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime DeadLine { get; set; }
    public string Priority { get; set; }

    public IEnumerable<Task> SubTasks { get; set; }
    public IEnumerable<User> AssignedTo { get; set; }
    public IEnumerable<Tag> Tags { get; set; }
}
