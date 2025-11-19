using Application.DTOs.Inputs.Auth;
using Application.DTOs.Outputs;
using Application.DTOs.Outputs.Auth;
using Application.Services.Interfaces;

namespace Application.Commands.Auth;

public class RenewRefreshTokenCommand(IAuthService authService)
{
    public async Task<OperationResultDto<UserAuthOutputDto>> Execute(RenewRefreshTokenInputDto dto)
    {
        return await authService.RefreshAccessTokenAsync(dto.RefreshToken, dto.IpAddress, dto.DeviceInfo);
    }
}
