using Domain.Data.Repositories;

namespace Application.Queries.Task; 

public class QueryAll
{
    private readonly ITaskRepository _taskRepository;

    public QueryAll(ITaskRepository taskRepository)
    {
        _taskRepository = taskRepository;
    }

    public async Task<IEnumerable<Domain.Models.Task>> Execute()
    {
        return await _taskRepository.Query();
    }
}
