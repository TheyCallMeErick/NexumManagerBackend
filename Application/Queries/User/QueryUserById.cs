using Domain.Data.Repositories;

namespace Application.Queries.User; 

public class QueryUserById
{
    private readonly IUserRepository _userRepository;

    public QueryUserById(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<Domain.Models.User> Execute(Guid id)
    {
        return await _userRepository.FindById(id) ?? throw new ArgumentOutOfRangeException();
    }

}
