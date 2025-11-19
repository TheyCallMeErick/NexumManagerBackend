using Microsoft.AspNetCore.Http;

namespace Application.DTOs.Inputs.Task; 

public record AttachFileToTaskDto(IFormFile File, Guid TaskId, Guid CurrentUser);