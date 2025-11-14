using Domain.Data.Repositories;
using Domain.Models;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories; 

public class UserRepository 
{
    private readonly ApplicationDbContext applicationDbContext;

    public UserRepository(ApplicationDbContext applicationDbContext)
    {
        this.applicationDbContext = applicationDbContext;
    }

    public async Task<User> Create(User User)
    {
        applicationDbContext.Users.Add(User);
        await applicationDbContext.SaveChangesAsync();
        return User;
    }

    public async Task<User> Update(User User)
    {
        applicationDbContext.Users.Update(User);
        await applicationDbContext.SaveChangesAsync();
        return User;
    }

    public async Task<User?> FindById(Guid id)
    {
        return await applicationDbContext.Users.FirstOrDefaultAsync(x=>x.Id == id);
    }

    public async Task<bool> Delete(User user)
    {
        applicationDbContext.Users.Remove(user);
        await applicationDbContext.SaveChangesAsync();
        return true;
    }
}
