using Application.Adapters;
using Application.DTOs.Inputs.Task;
using Domain.Data.Repositories;
using Domain.Enums;
using Domain.Models;

namespace Application.Commands.Task;

public class AttachFileToTaskCommand(IFileStorage fileStorageProvider, ITaskRepository taskRepository)
{

    public async Task<bool> Execute(AttachFileToTaskDto dto)
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
        var fileName = await fileStorageProvider.WriteFileAsync(dto.File.OpenReadStream(), dto.File.FileName);
        var attach = new Attach{
            FileName = fileName
        };
        task.Attaches.Add(attach);
        await taskRepository.Update(task);
        return true;
    }

}
