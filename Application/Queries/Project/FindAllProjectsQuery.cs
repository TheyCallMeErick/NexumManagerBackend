using Domain.Data.Repositories;

namespace Application.Queries.Project; 

public class FindAllProjectsQuery(IProjectRepository projectRepository)
{
    public async Task<IEnumerable<Domain.Models.Project>> Execute()
    {
        return await projectRepository.Query();
    }
}
