using Domain.Enums;

namespace Application.DTOs.Inputs.Project; 

public record InviteUserToProjectInputDTO(Guid CurrentUser, Guid UserInvited, Guid Project, EProjectRole Role);