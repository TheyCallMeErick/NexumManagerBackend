using Application.DTOs.Inputs.Task;
using Domain.Data.Repositories;
using Domain.Enums;

namespace Application.Commands.Task; 

public class UpdateTaskCommand
{
    private readonly ITagRepository _tagRepository;
    private readonly ITaskRepository _taskRepository;

    public UpdateTaskCommand(ITagRepository tagRepository, ITaskRepository taskRepository)
    {
        _tagRepository = tagRepository;
        _taskRepository = taskRepository;
    }

    public async Task<bool> Execute(UpdateTaskDTO dTO)
    {
        var task = await _taskRepository.FindById(dTO.TaskId);
        if (task == null)
        {
            return false;
        }

        foreach (var user in dTO.UsersAssigned)
        {
            if(!task.Project.Members.Select(x=>x.UserId).Contains(user))
            {
                return false;
            }
        }

        if (task.Project.Members.FirstOrDefault(x => x.UserId == dTO.UserCreating && (x.Role == EProjectRole.Admin || x.Role == EProjectRole.Manager)) == null)
        {
            return false;
        }

        var tags = await _tagRepository.FindManyById(dTO.Tags);
        if(tags.Count() != dTO.Tags.Count())
        {
            return false;
        }

        var members = task.Project.Members.Where(x => dTO.UsersAssigned.Contains(x.UserId ));

        await _taskRepository.Update(task);
        return true;
    }
}
