using Application.DTOs.Inputs.Project;
using Domain.Data.Repositories;

namespace Application.Commands.Project; 

public class UpdateProjectCommand(IProjectRepository projectRepository)
{
    public async Task<bool> Execute(UpdateProjectInputDto dto)
    {
        var project = await projectRepository.FindById(dto.ProjectId);
        if (project == null)
        {
            return false;
        }
        project.Title = dto.ProjectName;
        project.Description = dto.ProjectDescription;

        await projectRepository.Update(project);
        return true;
    }
}
