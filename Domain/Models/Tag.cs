namespace Domain.Models; 

public class Tag : BaseModel
{
    public string Description { get; set; } = string.Empty;
    public Guid ProjectId { get; set; }
    public Project Project { get; set; }
    public ICollection<Task> Tasks { get; set; } = new List<Task>();
}
