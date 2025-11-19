namespace Application.DTOs.Inputs.Task; 

public record DeleteTaskDto(Guid CurrentUser, Guid TaskId);