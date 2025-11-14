using Application.DTOs.Inputs.Project;
using Domain.Data.Repositories;
using Domain.Enums;
using Domain.Models;

namespace Application.Commands.Project; 

public class InviteUserToProjectCommand
{
    public readonly IProjectRepository _projectRepository;
    public readonly IUserRepository _userRepository;

    public InviteUserToProjectCommand(IProjectRepository projectRepository, IUserRepository userRepository)
    {
        _projectRepository = projectRepository;
        _userRepository = userRepository;
    }

    public async Task<bool> Execute(InviteUserToProjectInputDTO dto)
    {
        var project = await _projectRepository.FindById(dto.Project);
        if (project == null)
        {
            return false;
        }
        bool isAdminOrManager =
        project.Members
        .Any(x => x.UserId == dto.CurrentUser &&
                  (x.Role == EProjectRole.Admin || x.Role == EProjectRole.Manager));

        if (!isAdminOrManager)
        {
            return false;
        }
        if(project.Members.Any(x=>x.UserId == dto.UserInvited))
        {
            return false;
        }
        var user = await _userRepository.FindById(dto.UserInvited);
        if(user == null)
        {
            return false;
        }
        var invite = new UserInviteToProject
        {
            ProjectId = project.Id,
            UserId = user.Id,
            Role = dto.Role
        };

        project.Invites.Add(invite);
        return true;
    }
}
