using Domain.Models;

namespace Domain.Data.Repositories; 

public interface ITagRepository
{
    public  Task<Tag> Create(Tag tag);
    public  Task<Tag> Update(Tag tag);
    public  Task<Tag?> FindById(Guid id);
    public  Task<bool> Delete(Tag tag);
    public Task<IEnumerable<Tag>> Query();
    public  Task<IEnumerable<Tag>> FindManyById(IEnumerable<Guid> ids);
}
