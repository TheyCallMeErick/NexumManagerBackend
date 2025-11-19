using Domain.Data.Repositories;

namespace Application.Queries.Project; 

public class FindProjectsByIdQuery(IProjectRepository projectRepository)
{
    public async Task<Domain.Models.Project> Execute(Guid id)
    {
        return await projectRepository.FindById(id) ?? throw new ArgumentOutOfRangeException();
    }
}
