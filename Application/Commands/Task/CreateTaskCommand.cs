using Application.DTOs.Inputs.Task;
using Domain.Data.Repositories;
using Domain.Enums;

namespace Application.Commands.Task;

public class CreateTaskCommand
{
    private readonly IProjectRepository _projectRepository;
    private readonly ITagRepository _tagRepository;
    private readonly ITaskRepository _taskRepository;

    public CreateTaskCommand(IProjectRepository projectRepository, ITagRepository tagRepository, ITaskRepository taskRepository)
    {
        _projectRepository = projectRepository;
        _tagRepository = tagRepository;
        _taskRepository = taskRepository;
    }

    public async Task<bool> Execute(CreateTaskDTO dTO)
    {
        var project = await _projectRepository.FindById(dTO.ProjectId);
        if (project == null)
        {
            return false;
        }

        foreach (var user in dTO.UsersAssigned)
        {
            if(!project.Members.Select(x=>x.UserId).Contains(user))
            {
                return false;
            }
        }

        if (project.Members.FirstOrDefault(x => x.UserId == dTO.UserCreating && (x.Role == EProjectRole.Admin || x.Role == EProjectRole.Manager)) == null)
        {
            return false;
        }

        var tags = await _tagRepository.FindManyById(dTO.Tags);
        if(tags.Count() != dTO.Tags.Count())
        {
            return false;
        }

        var members = project.Members.Where(x => dTO.UsersAssigned.Contains(x.UserId )).Select(x=>x.User).ToList();

        var task = new Domain.Models.Task
        {
            AssignedTo = members,
            Title = dTO.Title,
        };
        task.Description = dTO.Description ?? "";
        task.Tags = tags.ToList();
        task.StartDate = dTO.StartDate;
        task.StartDate = dTO.DeadLine;
        task.Priority = dTO.Priority;
        await _taskRepository.Create(task);
        return true;
    }
}
