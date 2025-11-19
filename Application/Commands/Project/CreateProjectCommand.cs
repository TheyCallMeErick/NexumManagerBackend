using Application.DTOs.Inputs.Project;
using Domain.Data.Repositories;
using Domain.Enums;
using Domain.Models;

namespace Application.Commands.Project; 

public class CreateProjectCommand(IProjectRepository projectRepository, IUserRepository userRepository)
{
    public async Task<bool> Execute(CreateProjectInputDto dto)
    {
        if(dto.ProjectName.Length == 0)
        {
            return false;
        }
        var user = await userRepository.FindById(dto.CurrentUserId);

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

        await projectRepository.Create(project);
        return true;
    }
}
