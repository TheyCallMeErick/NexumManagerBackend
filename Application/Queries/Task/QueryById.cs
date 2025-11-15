using Domain.Data.Repositories;

namespace Application.Queries.Task; 

public class QueryById
{
    private readonly ITaskRepository _taskRepository;

    public QueryById(ITaskRepository taskRepository)
    {
        _taskRepository = taskRepository;
    }

    public async Task<Domain.Models.Task> Execute(Guid id)
    {
        return await _taskRepository.FindById(id) ?? throw new ArgumentOutOfRangeException();
    }
}
