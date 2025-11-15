using Application.DTOs.Outputs;
using Application.Services.Interfaces;

namespace Application.Commands.Auth;

public class RefreshAccessTokenCommand
{
    private readonly IAuthService _authService;

    public RefreshAccessTokenCommand(IAuthService authService)
    {
        _authService = authService;
    }

    public async Task<OperationResultDTO<string>> Execute(string refreshToken)
    {
        return await _authService.RefreshAccessTokenAsync(refreshToken);
    }
}
