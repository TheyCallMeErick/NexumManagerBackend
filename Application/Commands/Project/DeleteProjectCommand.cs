using System.Threading.Tasks;
using Domain.Data.Repositories;

namespace Application.Commands.Project; 

public class DeleteProjectCommand
{
    private readonly IProjectRepository _projectRepository;

    public async Task Execute(Guid projectId, Guid userId)
    {
        var project = await _projectRepository.FindById(projectId);
        if (project == null)
        {
            return;
        }
        await _projectRepository.Delete(project);
    }
}
