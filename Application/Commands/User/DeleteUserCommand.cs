using Domain.Data.Repositories;

namespace Application.Commands.User;

public class DeleteUserCommand(IUserRepository userRepository)
{
    public async Task<bool> Execute(Guid id)
    {
        var user = await userRepository.FindById(id);
        if (user == null)
        {
            return false;
        }
        return await userRepository.Delete(user);
    }

}
