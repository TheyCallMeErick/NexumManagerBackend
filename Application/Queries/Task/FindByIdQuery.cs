using Domain.Data.Repositories;

namespace Application.Queries.Task; 

public class FindByIdQuery
{
    private readonly ITaskRepository _taskRepository;

    public FindByIdQuery(ITaskRepository taskRepository)
    {
        _taskRepository = taskRepository;
    }

    public async Task<Domain.Models.Task> Execute(Guid id)
    {
        return await _taskRepository.FindById(id) ?? throw new ArgumentOutOfRangeException();
    }
}
