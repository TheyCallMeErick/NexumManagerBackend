using Domain.Data.Repositories;

namespace Application.Queries.Task; 

public class FindAllTasksQuery(ITaskRepository taskRepository)
{
    public async Task<IEnumerable<Domain.Models.Task>> Execute()
    {
        return await taskRepository.Query();
    }
}
