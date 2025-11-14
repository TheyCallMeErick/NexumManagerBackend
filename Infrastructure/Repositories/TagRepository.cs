using Domain.Data.Repositories;
using Domain.Models;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories; 

public class TagRepository : ITagRepository
{
    private readonly ApplicationDbContext applicationDbContext;

    public TagRepository(ApplicationDbContext applicationDbContext)
    {
        this.applicationDbContext = applicationDbContext;
    }

    public async Task<Tag> Create(Tag tag)
    {
        applicationDbContext.Tags.Add(tag);
        await applicationDbContext.SaveChangesAsync();
        return tag;
    }

    public async Task<Tag> Update(Tag tag)
    {
        applicationDbContext.Tags.Update(tag);
        await applicationDbContext.SaveChangesAsync();
        return tag;
    }

    public async Task<Tag?> FindById(Guid id)
    {
        return await applicationDbContext.Tags.FirstOrDefaultAsync(x=>x.Id == id);
    }

    public async Task<IEnumerable<Tag>> FindManyById(IEnumerable<Guid> ids)
    {
        return await applicationDbContext.Tags.Where(x=> ids.Contains(x.Id)).ToListAsync();
    }

    public async Task<bool> Delete(Tag tag)
    {
        applicationDbContext.Tags.Remove(tag);
        await applicationDbContext.SaveChangesAsync();
        return true;
    }

}
