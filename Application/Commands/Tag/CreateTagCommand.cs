using Application.DTOs.Inputs.Tag;
using Domain.Data.Repositories;
using Domain.Enums;

namespace Application.Commands.Tag;

public class CreateTagCommand(ITagRepository tagRepository, IProjectRepository projectRepository)
{
    public async Task<bool> Execute(CreateTagDto dto)
    {
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
        await tagRepository.Create(new Domain.Models.Tag
        {
            Description = dto.Description,
            Project =  project
        });
        return true;
    }
}


