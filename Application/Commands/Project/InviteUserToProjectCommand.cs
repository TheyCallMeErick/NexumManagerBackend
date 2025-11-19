using Application.DTOs.Inputs.Project;
using Domain.Data.Repositories;
using Domain.Enums;
using Domain.Models;

namespace Application.Commands.Project; 

public class InviteUserToProjectCommand(IProjectRepository projectRepository, IUserRepository userRepository)
{
    public async Task<bool> Execute(InviteUserToProjectInputDto dto)
    {
        var project = await projectRepository.FindById(dto.Project);
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
        var user = await userRepository.FindById(dto.UserInvited);
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
