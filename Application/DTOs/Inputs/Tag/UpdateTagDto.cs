namespace  Application.DTOs.Inputs.Tag;

public record UpdateTagDto(Guid ProjectId, Guid CurrentUserId,Guid TagId, string Description);