using Domain.Data.Repositories;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories; 

public class TaskRepository : ITaskRepository
{
    private readonly ApplicationDbContext applicationDbContext;

    public TaskRepository(ApplicationDbContext applicationDbContext)
    {
        this.applicationDbContext = applicationDbContext;
    }

    public async Task<Domain.Models.Task> Create(Domain.Models.Task task)
    {
        applicationDbContext.Tasks.Add(task);
        await applicationDbContext.SaveChangesAsync();
        return task;
    }

    public async Task<Domain.Models.Task> Update(Domain.Models.Task task)
    {
        applicationDbContext.Tasks.Update(task);
        await applicationDbContext.SaveChangesAsync();
        return task;
    }

    public async Task<Domain.Models.Task?> FindById(Guid id)
    {
        return await applicationDbContext.Tasks.Include(x=>x.Project).FirstOrDefaultAsync(x=>x.Id == id);
    }

    public async Task<bool> Delete(Domain.Models.Task task)
    {
        applicationDbContext.Tasks.Remove(task);
        await applicationDbContext.SaveChangesAsync();
        return true;
    }
}
