using Application.DTOs.Inputs.Auth;
using Application.DTOs.Outputs;
using Application.DTOs.Outputs.Auth;
using Application.Services.Interfaces;

namespace Application.Commands.Auth;

public class RenewRefreshTokenCommand
{
    private readonly IAuthService _authService;

    public RenewRefreshTokenCommand(IAuthService authService)
    {
        _authService = authService;
    }

    public async Task<OperationResultDTO<UserAuthOutputDTO>> Execute(RenewRefreshTokenInputDTO dto)
    {
        return await _authService.RefreshAccessTokenAsync(dto.refreshToken, dto.ipAddress, dto.deviceInfo);
    }
}
