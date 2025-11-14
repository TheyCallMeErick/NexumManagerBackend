using Domain.Models;

namespace Domain.Data.Repositories; 

public interface IUserRepository
{
    public Task<User> Create(User user);
    public Task<User> Update(User user);
    public Task<User?> FindById(Guid id);
    public Task<bool> Delete(User user);

}
