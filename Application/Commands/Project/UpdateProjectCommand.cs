using System.Threading.Tasks;
using Application.DTOs.Inputs.Project;
using Domain.Data.Repositories;

namespace Application.Commands.Project; 

public class UpdateProjectCommand
{
    private readonly IProjectRepository _projectRepository;

    public async Task<bool> Execute(UpdateProjectInputDTO dto)
    {
        var project = await _projectRepository.FindById(dto.projectId);
        if (project == null)
        {
            return false;
        }
        project.Title = dto.ProjectName;
        project.Description = dto.ProjectDescription;

        await _projectRepository.Update(project);
        return true;
    }
}
