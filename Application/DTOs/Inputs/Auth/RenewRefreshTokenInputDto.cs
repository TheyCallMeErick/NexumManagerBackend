namespace Application.DTOs.Inputs.Auth; 

public record RenewRefreshTokenInputDto(Guid RefreshToken, string IpAddress, string DeviceInfo);