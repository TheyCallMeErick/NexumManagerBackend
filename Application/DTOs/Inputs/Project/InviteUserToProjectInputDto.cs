using Domain.Enums;

namespace Application.DTOs.Inputs.Project; 

public record InviteUserToProjectInputDto(Guid CurrentUser, Guid UserInvited, Guid Project, EProjectRole Role);