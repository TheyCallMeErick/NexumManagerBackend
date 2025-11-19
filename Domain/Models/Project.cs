namespace Domain.Models; 

public class Project : BaseModel
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public ICollection<UserOnProject> Members { get; set; } = new List<UserOnProject>();
    public ICollection<Tag> Tags { get; set; } = new List<Tag>();

    public ICollection<UserInviteToProject> Invites { get; set; } = new List<UserInviteToProject>();
}
