namespace  Application.DTOs.Inputs.Tag;

public record CreateTagDto(Guid ProjectId, Guid CurrentUserId, string Description);