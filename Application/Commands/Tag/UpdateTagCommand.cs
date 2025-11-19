using Application.DTOs.Inputs.Tag;
using Domain.Data.Repositories;
using Domain.Enums;

namespace Application.Commands.Tag; 

public class UpdateTagCommand(ITagRepository tagRepository, IProjectRepository projectRepository)
{
    public async Task<bool> Execute(UpdateTagDto dto)
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

        var canCurrentUserCreateTag = project
            .Members
            .Any(m => m.UserId == dto.CurrentUserId
                      && m.Role is EProjectRole.Admin or EProjectRole.Manager
            );
        if (!canCurrentUserCreateTag)
        {
            return false;
        }
        tag.Description = dto.Description;
        await tagRepository.Update(tag);
        return true;
    }
}



