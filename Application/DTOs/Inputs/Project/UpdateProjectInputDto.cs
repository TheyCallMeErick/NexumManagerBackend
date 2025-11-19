namespace Application.DTOs.Inputs.Project; 

public record UpdateProjectInputDto(Guid ProjectId, Guid UserId, string ProjectName = "", string ProjectDescription = "");