using Domain.Data.Repositories;

namespace Application.Queries.Project; 

public class QueryAllProjects
{
    private readonly IProjectRepository _projectRepository;

    public QueryAllProjects()
    {
        // constructor logic here
    }

    public async Task<IEnumerable<Domain.Models.Project>> Execute()
    {
        return await _projectRepository.Query();
    }
}
