using Microsoft.AspNetCore.Identity;

namespace Domain.Models; 

public class User : BaseModel
{

    public string? Username { get; set; }
    public string? Name { get; set; }
    public string? Email { get; set; }
    public string? PasswordHash { get; set; }
    public string? ProfilePictureFileName { get; set; }
    public bool EnableNotifications {get;set;}

    public ICollection<RefreshToken> RefreshTokens { get; set; } = new List<RefreshToken>();
    public ICollection<UserInviteToProject> Invites { get; set; } = new List<UserInviteToProject>();
}
