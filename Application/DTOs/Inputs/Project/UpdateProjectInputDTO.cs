namespace Application.DTOs.Inputs.Project; 

public record UpdateProjectInputDTO(Guid projectId, Guid userId, string ProjectName = "", string ProjectDescription = "");