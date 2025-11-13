namespace Domain.Models; 

public class UserOnProject : BaseModel
{
    public Roles Role { get; set; }
    public Guid RoleId { get; set; }
    public User User { get; set; }
    public Guid UserId { get; set; }
    public Project Project { get; set; }
    public Guid ProjectId { get; set; }

}
