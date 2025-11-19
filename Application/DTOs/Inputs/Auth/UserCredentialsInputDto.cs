namespace Application.DTOs.Inputs.Auth;

public record UserCredentialsInputDto(
    string Email,
    string Password,
    string? Ip,
    string? DeviceInfo);