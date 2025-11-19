using Domain.Data.Repositories;

namespace Application.Queries.Tag;
public sealed class FindTagByIdQuery(ITagRepository tagRepository)
{
    public async Task<Domain.Models.Tag> Execute(Guid id)
    {
        return await tagRepository.FindById(id) ?? throw new ArgumentOutOfRangeException();
    }
}
