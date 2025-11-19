using Application.DTOs.Inputs.Auth;
using Application.DTOs.Outputs;
using Application.DTOs.Outputs.Auth;
using Application.Services.Interfaces;

namespace Application.Commands.Auth;

public class ValidateUserCredentialsCommand(IAuthService authService)
{
    public async Task<OperationResultDto<UserAuthOutputDto>> Execute(UserCredentialsInputDto dto)
    {
        return await authService.ValidateUserCredentialsAsync(dto); 
    }
}
