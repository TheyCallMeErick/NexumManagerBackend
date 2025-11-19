using Application.DTOs.Inputs.Task;
using Domain.Data.Repositories;
using Domain.Enums;

namespace Application.Commands.Task; 

public class DeleteTaskCommand(ITaskRepository taskRepository)
{
    public async Task<bool> Execute(DeleteTaskDto dto)
    {
        var task = await taskRepository.FindById(dto.TaskId);
        if (task == null)
        {
            return false;
        }
        bool isAdminOrManager =
        task.Project.Members
        .Any(x => x.UserId == dto.CurrentUser &&
                  (x.Role == EProjectRole.Admin || x.Role == EProjectRole.Manager));

        if (!isAdminOrManager)
        {
            return false;
        }

        await taskRepository.Delete(task);
        return true;
    }
}
