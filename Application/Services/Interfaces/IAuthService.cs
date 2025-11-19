
using Application.DTOs.Inputs.Auth;
using Application.DTOs.Outputs;
using Application.DTOs.Outputs.Auth;
using Application.DTOs.Outputs.User;

namespace Application.Services.Interfaces;
public interface IAuthService
{
    Task<OperationResultDto<string>> RefreshAccessTokenAsync(string refreshToken);
    Task<OperationResultDto<UserAuthOutputDto>> ValidateUserCredentialsAsync(UserCredentialsInputDto dto);
    Task<OperationResultDto<UserAuthOutputDto>> RefreshAccessTokenAsync(Guid refreshToken, string ipAddress, string deviceInfo);
    Task<OperationResultDto<UserOutputDto>> GetCurrentUserAsync(string userId);
}