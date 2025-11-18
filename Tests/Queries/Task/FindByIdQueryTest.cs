using Application.Queries.Task;
using Domain.Data.Repositories;
using Infrastructure.Data;
using Infrastructure.Repositories;
using Tests.Utils;

namespace Tests.Queries.Task; 

public class FindByIdQueryTest
{
    private readonly ApplicationDbContext _dbContext;
    private readonly ITaskRepository _taskRepository;
    private readonly FindByIdQuery _findByIdQuery;

    public FindByIdQueryTest()
    {
        _dbContext = CreateInMemoryDatabase.Handle();
        _taskRepository = new TaskRepository(_dbContext);
        _findByIdQuery = new FindByIdQuery(_taskRepository);
    }
}
