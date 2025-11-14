using Domain.Enums;

namespace Domain.Models; 

public class UserOnProject : BaseModel
{
    public EProjectRole Role { get; set; }
    public User User { get; set; }
    public Guid UserId { get; set; }
    public Project Project { get; set; }
    public Guid ProjectId { get; set; }

}
