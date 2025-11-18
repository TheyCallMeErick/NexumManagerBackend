using Domain.Data.Repositories;

namespace Application.Queries.Project; 

public class FindProjectsByIdQuery
{
    private readonly IProjectRepository _projectRepository;

    public FindProjectsByIdQuery()
    {
        // constructor logic here
    }

    public async Task<Domain.Models.Project> Execute(Guid id)
    {
        return await _projectRepository.FindById(id) ?? throw new ArgumentOutOfRangeException();
    }
}
