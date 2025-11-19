using Domain.Data.Repositories;

namespace Application.Queries.Task; 

public class FindByIdQuery(ITaskRepository taskRepository)
{
    public async Task<Domain.Models.Task> Execute(Guid id)
    {
        return await taskRepository.FindById(id) ?? throw new ArgumentOutOfRangeException();
    }
}
