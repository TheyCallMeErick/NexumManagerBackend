namespace Application.DTOs.Inputs.Task; 

public record DeleteTaskDTO(Guid CurrentUser, Guid TaskId);