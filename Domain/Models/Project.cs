namespace Domain.Models; 

public class Project : BaseModel
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public IEnumerable<UserOnProject> Members { get; set; } = new List<UserOnProject>();

}
