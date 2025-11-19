using Domain.Data.Repositories;
using Domain.Enums;

namespace Application.Commands.Project; 

public class DeleteProjectCommand(IProjectRepository projectRepository)
{
    public async Task<bool> Execute(Guid projectId, Guid userId)
    {
        var project = await projectRepository.FindById(projectId);
        if (project == null)
        {
            return false;
        }
        var isCurrentUserAdmin = project.Members.Any(x=>x.UserId == userId && (x.Role == EProjectRole.Admin || x.Role == EProjectRole.Manager));
        if (!isCurrentUserAdmin)
        {
            return false;
        }
        await projectRepository.Delete(project);
        return true;
    }
}
