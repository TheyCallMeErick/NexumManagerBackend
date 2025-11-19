using Application.DTOs.Inputs.Task;
using Domain.Data.Repositories;
using Domain.Enums;

namespace Application.Commands.Task;

public class UpdateTaskCommand(ITagRepository tagRepository, ITaskRepository taskRepository)
{
    public async Task<bool> Execute(UpdateTaskDto dto)
    {
        var task = await taskRepository.FindById(dto.TaskId);
        if (task == null)
        {
            return false;
        }


        if (dto.UsersAssigned != null)
        {
            foreach (var user in dto.UsersAssigned)
            {
                if (!task.Project.Members.Select(x => x.UserId).Contains(user))
                {
                    return false;
                }
            }
        }


        if (task.Project.Members.FirstOrDefault(x =>
                x.UserId == dto.UserCreating && (x.Role == EProjectRole.Admin || x.Role == EProjectRole.Manager)) ==
            null)
        {
            return false;
        }

        if (dto.Tags != null)
        {
            var tags = await tagRepository.FindManyById(dto.Tags);
            if (tags.Count() != dto.Tags.Count())
            {
                return false;
            }
        }

        var members = task.Project.Members.Where(x => dto.UsersAssigned != null && dto.UsersAssigned.Contains(x.UserId));

        await taskRepository.Update(task);
        return true;
    }
}
