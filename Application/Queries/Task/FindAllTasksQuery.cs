using Domain.Data.Repositories;

namespace Application.Queries.Task; 

public class FindAllTasksQuery
{
    private readonly ITaskRepository _taskRepository;

    public FindAllTasksQuery(ITaskRepository taskRepository)
    {
        _taskRepository = taskRepository;
    }

    public async Task<IEnumerable<Domain.Models.Task>> Execute()
    {
        return await _taskRepository.Query();
    }
}
