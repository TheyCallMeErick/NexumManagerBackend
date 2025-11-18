using Application.Queries.Task;
using Domain.Data.Repositories;
using Infrastructure.Data;
using Infrastructure.Repositories;
using Tests.Utils;

namespace Tests.Queries.Task; 

public class FindAllTasksQueryTest
{
    private readonly ApplicationDbContext _dbContext;
    private readonly ITaskRepository _taskRepository;
    private readonly FindAllTasksQuery _findAllTasksQuery;

    public FindAllTasksQueryTest()
    {
        _dbContext = CreateInMemoryDatabase.Handle();
        _taskRepository = new TaskRepository(_dbContext);
        _findAllTasksQuery = new FindAllTasksQuery(_taskRepository);
    }
}
