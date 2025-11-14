using System.Threading.Tasks;
using Domain.Data.Repositories;

namespace Application.Commands.Project; 

public class DeleteProjectCommand
{
    private readonly IProjectRepository _projectRepository;

    public DeleteProjectCommand(IProjectRepository projectRepository)
    {
        _projectRepository = projectRepository;
    }

    public async Task<bool> Execute(Guid projectId, Guid userId)
    {
        var project = await _projectRepository.FindById(projectId);
        if (project == null)
        {
            return false;
        }
        await _projectRepository.Delete(project);
        return true;
    }
}
