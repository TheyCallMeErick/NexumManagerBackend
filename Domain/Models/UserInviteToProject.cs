using Domain.Enums;

namespace Domain.Models; 

public class UserInviteToProject : BaseModel
{
    public Guid UserId { get; set; }
    public User User { get; set; }
    public Guid ProjectId { get; set; }
    public Project Project { get; set; }
    public EProjectRole Role { get; set; }
    public bool IsCanceled {get;set;} = false;
    public bool IsAccepted {get;set;} = false;
    public DateTime SendAt { get; set; } = DateTime.Now;
}
