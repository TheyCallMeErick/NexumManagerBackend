namespace Application.DTOs.Inputs.Project; 

public record CreateProjectInputDto(string ProjectName, Guid CurrentUserId,string ProjectDescription = "");