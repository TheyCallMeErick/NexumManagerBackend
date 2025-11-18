using Domain.Models;

namespace Domain.Data.Repositories;

public interface IUserOnProjectRepository
{
    public Task<UserOnProject> Create(UserOnProject userOnProject);
    public Task<UserOnProject> Update(UserOnProject userOnProject);
    public Task<UserOnProject?> FindById(Guid userOnProject);
    public  Task<IEnumerable<UserOnProject>> Query();
    public Task<bool> Delete(UserOnProject userOnProject);

}
