using Application.DTOs.Inputs.Project;
using Domain.Data.Repositories;
using Domain.Models;

namespace Application.Commands.Project; 

public class AcceptInviteToProjectCommand(IUserRepository userRepository, IUserInviteRepository userInviteRepository, IProjectRepository projectRepository)
{
    public async Task<bool> Execute(AcceptInviteToProjectDto dto)
    {
        var user = await userRepository.FindById(dto.CurrentUser);
        if (user == null)
        {
            return false;
        }

        var invite = await userInviteRepository.FindById(dto.InviteId);
        if (invite == null)
        {
            return false;
        }
        if(invite.User.Id != user.Id)
        {
            return false;
        }
        var project = await projectRepository.FindById(invite.ProjectId);
        if(project == null)
        {
            return false;
        }

        var userOnProject = new UserOnProject
        {
            Role = invite.Role,
            Project = project,
            User = invite.User
        };

        project.Members.Add(userOnProject);
        await projectRepository.Update(project);
        return true;
    }
}
