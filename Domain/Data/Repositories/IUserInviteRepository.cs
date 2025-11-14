using Domain.Models;

namespace Domain.Data.Repositories;

public interface IUserInviteRepository
{
    public Task<UserInviteToProject> Create(UserInviteToProject UserInvite);
    public Task<UserInviteToProject> Update(UserInviteToProject UserInvite);
    public Task<UserInviteToProject?> FindById(Guid id);
    public Task<IEnumerable<UserInviteToProject>> FindManyById(IEnumerable<Guid> ids);
    public Task<bool> Delete(UserInviteToProject UserInvite);

}
