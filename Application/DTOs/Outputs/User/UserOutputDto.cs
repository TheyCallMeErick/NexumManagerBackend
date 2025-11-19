namespace Application.DTOs.Outputs.User; 

public record UserOutputDto(
    Guid Id,
    string? Username,
    string? Name,
    string? Email,
    string? ProfilePictureFileName
);