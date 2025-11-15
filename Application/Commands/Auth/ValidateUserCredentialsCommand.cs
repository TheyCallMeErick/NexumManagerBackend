using Application.DTOs.Inputs.Auth;
using Application.DTOs.Outputs;
using Application.DTOs.Outputs.Auth;
using Application.Services.Interfaces;

namespace Application.Commands.Auth;

public class ValidateUserCredentialsCommand
{
    private readonly IAuthService _authService;

    public ValidateUserCredentialsCommand(IAuthService authService)
    {
        _authService = authService;
    }

    public async Task<OperationResultDTO<UserAuthOutputDTO>> Execute(UserCredentialsInputDTO dto)
    {
        return await _authService.ValidateUserCredentialsAsync(dto); 
    }
}
