using Domain.Models;

namespace Domain.Data.Repositories;

public interface IProjectRepository
{
    public Task<Project> Create(Project project);
    public Task<Project> Update(Project project);
    public Task<Project?> FindById(Guid id);
    public Task<bool> Delete(Project project);

}
