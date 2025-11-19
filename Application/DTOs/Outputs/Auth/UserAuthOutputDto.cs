namespace Application.DTOs.Outputs.Auth;

public record UserAuthOutputDto(
    string AccessToken,
    string RefreshToken);