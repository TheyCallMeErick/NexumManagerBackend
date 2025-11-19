using Application.DTOs.Inputs.Tag;
using Domain.Data.Repositories;
using Domain.Enums;

namespace Application.Commands.Tag; 

public class DeleteTagCommand(ITagRepository tagRepository, IProjectRepository projectRepository)
{
    public async Task<bool> Execute(DeleteTagDto dto)
    {
        var tag = await tagRepository.FindById(dto.TagId);
        if (tag == null)
        {
            return false;
        }
        var project = await projectRepository.FindById(dto.ProjectId);
        if (project == null)
        {
            return false;
        }

        var canCurrentUserDeleteTag = project
            .Members
            .Any(m => m.UserId == dto.CurrentUserId
                      && m.Role is EProjectRole.Admin or EProjectRole.Manager
            );
        if (!canCurrentUserDeleteTag)
        {
            return false;
        }
        await tagRepository.Delete(tag);
        return true;
    }
}


