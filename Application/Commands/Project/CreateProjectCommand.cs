using System.ComponentModel;
using System.Threading.Tasks;
using Application.DTOs.Inputs.Project;
using Domain.Data.Repositories;

namespace Application.Commands.Project; 

public class CreateProjectCommand
{
    private readonly IProjectRepository _projectRepository;

    public CreateProjectCommand(IProjectRepository projectRepository)
    {
        _projectRepository = projectRepository;
    }

    public async Task<bool> Execute(CreateProjectInputDTO dto)
    {
        if(dto.ProjectName is null || dto.ProjectName.Length == 0)
        {
            return false;
        }

        var model = new Domain.Models.Project
        {
            Title = dto.ProjectName,
            Description = dto.ProjectDescription
        };

        await _projectRepository.Create(model);
        return true;
    }
}
