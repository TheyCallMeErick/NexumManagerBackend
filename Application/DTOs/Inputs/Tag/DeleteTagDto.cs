namespace  Application.DTOs.Inputs.Tag;

public record DeleteTagDto(Guid ProjectId, Guid CurrentUserId, Guid TagId);