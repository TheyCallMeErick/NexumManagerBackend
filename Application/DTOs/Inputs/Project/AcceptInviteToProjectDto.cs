namespace Application.DTOs.Inputs.Project; 

public record AcceptInviteToProjectDto(Guid CurrentUser, Guid InviteId);