using Application.DTOs.Inputs.Task;
using Domain.Data.Repositories;
using Domain.Enums;

namespace Application.Commands.Task;

public class CreateTaskCommand(IProjectRepository projectRepository, ITagRepository tagRepository, ITaskRepository taskRepository)
{
    public async Task<bool> Execute(CreateTaskDto dto)
    {
        var project = await projectRepository.FindById(dto.ProjectId);
        if (project == null)
        {
            return false;
        }

        foreach (var user in dto.UsersAssigned)
        {
            if(!project.Members.Select(x=>x.UserId).Contains(user))
            {
                return false;
            }
        }

        if (project.Members.FirstOrDefault(x => x.UserId == dto.UserCreating && (x.Role == EProjectRole.Admin || x.Role == EProjectRole.Manager)) == null)
        {
            return false;
        }

        var tags = await tagRepository.FindManyById(dto.Tags);
        var enumerable = tags as Domain.Models.Tag[] ?? tags.ToArray();
        if(enumerable.Length != dto.Tags.Count())
        {
            return false;
        }

        var members = project.Members.Where(x => dto.UsersAssigned.Contains(x.UserId )).Select(x=>x.User).ToList();

        var task = new Domain.Models.Task
        {
            AssignedTo = members,
            Title = dto.Title,
            Description = dto.Description ?? "",
            Tags = enumerable.ToList(),
            StartDate = dto.StartDate
        };
        task.StartDate = dto.DeadLine;
        task.Priority = dto.Priority;
        await taskRepository.Create(task);
        return true;
    }
}
