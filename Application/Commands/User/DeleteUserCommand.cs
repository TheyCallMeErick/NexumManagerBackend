using Domain.Data.Repositories;

namespace Application.Commands.User;

public class DeleteUserCommand
{
    private readonly IUserRepository _userRepository;

    public DeleteUserCommand(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<bool> Execute(Guid id)
    {
        var user = await _userRepository.FindById(id);
        if (user == null)
        {
            return false;
        }
        return await _userRepository.Delete(user);
    }

}
