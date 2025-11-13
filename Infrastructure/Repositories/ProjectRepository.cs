using System.Threading.Tasks;
using Domain.Data.Repositories;
using Domain.Models;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories; 

public class ProjectRepository : IProjectRepository
{
    private readonly ApplicationDbContext applicationDbContext;

    public ProjectRepository(ApplicationDbContext applicationDbContext)
    {
        this.applicationDbContext = applicationDbContext;
    }

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
        return await applicationDbContext.Projects.FirstOrDefaultAsync(x=>x.Id == id);
    }

    public async Task<bool> Delete(Project project)
    {
        applicationDbContext.Projects.Remove(project);
        await applicationDbContext.SaveChangesAsync();
        return true;
    }

}
