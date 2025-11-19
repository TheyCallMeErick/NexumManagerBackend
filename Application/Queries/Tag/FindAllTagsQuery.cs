using Domain.Data.Repositories;

namespace Application.Queries.Tag;
public sealed class FindAllTagsQuery(ITagRepository tagRepository)
{
    public async Task<IEnumerable<Domain.Models.Tag>> Execute()
    {
        return await tagRepository.Query();
    }
}
