using Microsoft.AspNetCore.Http;

namespace Application.DTOs.Inputs.User;

public record UpdateSelfDataDTO(Guid Id, string? Username, string? Name, string? Email, string? Password, IFormFile? ProfilePicture, bool EnableNotifications = false);