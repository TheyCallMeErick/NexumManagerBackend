using Domain.Data.Repositories;
using Domain.Models;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories; 

public class UserInviteRepository(ApplicationDbContext applicationDbContext) : IUserInviteRepository
{
    public async Task<UserInviteToProject> Create(UserInviteToProject UserInvite)
    {
        applicationDbContext.UserInvites.Add(UserInvite);
        await applicationDbContext.SaveChangesAsync();
        return UserInvite;
    }

    public async Task<UserInviteToProject> Update(UserInviteToProject UserInvite)
    {
        applicationDbContext.UserInvites.Update(UserInvite);
        await applicationDbContext.SaveChangesAsync();
        return UserInvite;
    }

    public async Task<UserInviteToProject?> FindById(Guid id)
    {
        return await applicationDbContext.UserInvites.Include(x=>x.User).FirstOrDefaultAsync(x=>x.Id == id);
    }

    public async Task<IEnumerable<UserInviteToProject>> FindManyById(IEnumerable<Guid> ids)
    {
        return await applicationDbContext.UserInvites.Where(x=> ids.Contains(x.Id)).ToListAsync();
    }

    public async Task<bool> Delete(UserInviteToProject UserInvite)
    {
        applicationDbContext.UserInvites.Remove(UserInvite);
        await applicationDbContext.SaveChangesAsync();
        return true;
    }
}
