using Application.DTOs.Outputs;
using Application.Services.Interfaces;

namespace Application.Commands.Auth;

public class RefreshAccessTokenCommand(IAuthService authService)
{
    public async Task<OperationResultDto<string>> Execute(string refreshToken)
    {
        return await authService.RefreshAccessTokenAsync(refreshToken);
    }
}
