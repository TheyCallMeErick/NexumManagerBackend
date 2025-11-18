using System.ComponentModel;
using System.Threading.Tasks;
using Application.DTOs.Inputs.Project;
using Domain.Data.Repositories;
using Domain.Enums;
using Domain.Models;

namespace Application.Commands.Project; 

public class CreateProjectCommand
{
    private readonly IProjectRepository _projectRepository;
    private readonly IUserRepository _userRepository;
    public CreateProjectCommand(IProjectRepository projectRepository, IUserRepository userRepository)
    {
        _projectRepository = projectRepository;
        _userRepository = userRepository;
    }

    public async Task<bool> Execute(CreateProjectInputDTO dto)
    {
        if(dto.ProjectName is null || dto.ProjectName.Length == 0)
        {
            return false;
        }
        var user = await _userRepository.FindById(dto.CurrentUserId);

        if (user == null)
        {
            return false;
        }

        var project = new Domain.Models.Project
        {
            Title = dto.ProjectName,
            Description = dto.ProjectDescription
        };
        var userOnProject = new UserOnProject
        {
            Role = EProjectRole.Admin,
            User = user,
        };
        project.Members.Add(userOnProject);

        await _projectRepository.Create(project);
        return true;
    }
}
