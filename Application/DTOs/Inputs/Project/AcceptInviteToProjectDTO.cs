namespace Application.DTOs.Inputs.Project; 

public record AcceptInviteToProjectDTO(Guid CurrentUser, Guid InviteId);