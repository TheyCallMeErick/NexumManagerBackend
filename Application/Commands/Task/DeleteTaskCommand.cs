using Application.DTOs.Inputs.Task;
using Domain.Data.Repositories;
using Domain.Enums;

namespace Application.Commands.Task; 

public class DeleteTaskCommand
{
    private readonly ITaskRepository _taskRepository;

    public DeleteTaskCommand(ITaskRepository taskRepository)
    {
        _taskRepository = taskRepository;
    }

    public async Task<bool> Execute(DeleteTaskDTO dTO)
    {
        var task = await _taskRepository.FindById(dTO.TaskId);
        if (task == null)
        {
            return false;
        }
        bool isAdminOrManager =
        task.Project.Members
        .Any(x => x.UserId == dTO.CurrentUser &&
                  (x.Role == EProjectRole.Admin || x.Role == EProjectRole.Manager));

        if (!isAdminOrManager)
        {
            return false;
        }

        await _taskRepository.Delete(task);
        return true;
    }
}
