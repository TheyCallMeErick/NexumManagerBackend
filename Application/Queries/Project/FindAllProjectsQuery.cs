using Domain.Data.Repositories;

namespace Application.Queries.Project; 

public class FindAllProjectsQuery
{
    private readonly IProjectRepository _projectRepository;

    public FindAllProjectsQuery()
    {
        // constructor logic here
    }

    public async Task<IEnumerable<Domain.Models.Project>> Execute()
    {
        return await _projectRepository.Query();
    }
}
