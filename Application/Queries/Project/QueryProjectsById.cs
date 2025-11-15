using Domain.Data.Repositories;

namespace Application.Queries.Project; 

public class QueryProjectsById
{
    private readonly IProjectRepository _projectRepository;

    public QueryProjectsById()
    {
        // constructor logic here
    }

    public async Task<Domain.Models.Project> Execute(Guid id)
    {
        return await _projectRepository.FindById(id) ?? throw new ArgumentOutOfRangeException();
    }
}
