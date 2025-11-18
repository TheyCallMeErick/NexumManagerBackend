namespace Application.DTOs.Inputs.Project; 

public record CreateProjectInputDTO(string ProjectName, Guid CurrentUserId,string ProjectDescription = "");