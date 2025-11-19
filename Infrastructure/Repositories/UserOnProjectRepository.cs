using Domain.Data.Repositories;
using Domain.Models;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories; 

public class UserOnProjectRepository(ApplicationDbContext applicationDbContext) : IUserOnProjectRepository
{
    public async Task<UserOnProject> Create(UserOnProject userOnProject)
    {
        applicationDbContext.UsersOnProjects.Add(userOnProject);
        await applicationDbContext.SaveChangesAsync();
        return userOnProject;
    }

    public async Task<UserOnProject> Update(UserOnProject userOnProject)
    {
        applicationDbContext.UsersOnProjects.Update(userOnProject);
        await applicationDbContext.SaveChangesAsync();
        return userOnProject;
    }

    public async Task<UserOnProject?> FindById(Guid id)
    {
        return await applicationDbContext.UsersOnProjects.FirstOrDefaultAsync(x=>x.Id == id);
    }

    public async Task<bool> Delete(UserOnProject user)
    {
        applicationDbContext.UsersOnProjects.Remove(user);
        await applicationDbContext.SaveChangesAsync();
        return true;
    }

    public async Task<IEnumerable<UserOnProject>> Query()
    {
        return await applicationDbContext.UsersOnProjects.ToListAsync();
    }
}
