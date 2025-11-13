namespace Application.DTOs.Inputs.Project; 

public record InviteUserToProjectInputDTO(Guid InvitedBy, Guid UserInvited, Guid Project);