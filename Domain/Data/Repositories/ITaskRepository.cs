namespace Domain.Data.Repositories; 

public interface ITaskRepository
{
    public  Task<Models.Task> Create(Models.Task task);
    public  Task<Models.Task> Update(Models.Task task);
    public  Task<Models.Task?> FindById(Guid id);
    public  Task<IEnumerable<Models.Task>> Query();

    public  Task<bool> Delete(Models.Task task);

}
