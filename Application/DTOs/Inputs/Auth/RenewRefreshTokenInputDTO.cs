namespace Application.DTOs.Inputs.Auth; 

public record RenewRefreshTokenInputDTO(Guid refreshToken, string ipAddress, string deviceInfo);