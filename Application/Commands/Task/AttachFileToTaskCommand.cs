using Application.Adapters;
using Application.DTOs.Inputs.Task;
using Domain.Data.Repositories;
using Domain.Enums;
using Domain.Models;

namespace Application.Commands.Task;

public class AttachFileToTaskCommand
{
    private readonly IFileStorage _fileStorageProvider;
    private readonly ITaskRepository _taskRepository;

    public AttachFileToTaskCommand(IFileStorage fileStorageProvider, ITaskRepository taskRepository)
    {
        _fileStorageProvider = fileStorageProvider;
        _taskRepository = taskRepository;
    }

    public async Task<bool> Execute(AttachFileToTaskDTO dTO)
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
        var fileName = await _fileStorageProvider.WriteFileAsync(dTO.File.OpenReadStream(), dTO.File.FileName);
        var Attach = new Attach{
            FileName = fileName
        };
        task.Attaches.Append(Attach);
        await _taskRepository.Update(task);
        return true;
    }

}
