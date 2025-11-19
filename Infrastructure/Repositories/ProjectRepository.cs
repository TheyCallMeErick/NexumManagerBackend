using System.Threading.Tasks;
using Domain.Data.Repositories;
using Domain.Models;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories; 

public class ProjectRepository(ApplicationDbContext applicationDbContext) : IProjectRepository
{
    public async Task<Project> Create(Project project)
    {
        applicationDbContext.Projects.Add(project);
        await applicationDbContext.SaveChangesAsync();
        return project;
    }

    public async Task<Project> Update(Project project)
    {
        applicationDbContext.Projects.Update(project);
        await applicationDbContext.SaveChangesAsync();
        return project;
    }

    public async Task<Project?> FindById(Guid id)
    {
        return await applicationDbContext.Projects.Include(x=>x.Members).FirstOrDefaultAsync(x=>x.Id == id);
    }

    public async Task<bool> Delete(Project project)
    {
        applicationDbContext.Projects.Remove(project);
        await applicationDbContext.SaveChangesAsync();
        return true;
    }

    public async Task<IEnumerable<Project>> Query()
    {
        return await applicationDbContext.Projects.ToListAsync();
    }
}
