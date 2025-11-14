using Microsoft.AspNetCore.Http;

namespace Application.DTOs.Inputs.Task; 

public record AttachFileToTaskDTO(IFormFile File, Guid TaskId, Guid CurrentUser);