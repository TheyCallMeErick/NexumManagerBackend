using Application.DTOs.Inputs.Project;
using Domain.Data.Repositories;
using Domain.Models;

namespace Application.Commands.Project; 

public class AcceptInviteToProjectCommand
{
    private readonly  IUserRepository _userRepository;
    private readonly  IUserInviteRepository _userInviteRepository;
    private readonly  IProjectRepository _projectRepository;

    public AcceptInviteToProjectCommand(IUserRepository userRepository, IUserInviteRepository userInviteRepository, IProjectRepository projectRepository)
    {
        _userRepository = userRepository;
        _userInviteRepository = userInviteRepository;
        _projectRepository = projectRepository;
    }

    public async Task<bool> Execute(AcceptInviteToProjectDTO dto)
    {
        var user = await _userRepository.FindById(dto.CurrentUser);
        if (user == null)
        {
            return false;
        }

        var invite = await _userInviteRepository.FindById(dto.InviteId);
        if (invite == null)
        {
            return false;
        }
        if(invite.User.Id != user.Id)
        {
            return false;
        }
        var project = await _projectRepository.FindById(invite.ProjectId);
        if(project == null)
        {
            return false;
        }

        var UserOnProject = new UserOnProject
        {
            Role = invite.Role,
            Project = project,
            User = invite.User
        };

        project.Members.Append(UserOnProject);
        await _projectRepository.Update(project);
        return true;
    }
}
