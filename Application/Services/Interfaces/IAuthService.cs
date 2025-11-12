namespace Application.Services.Interfaces;

using Application.DTOs.Inputs.Auth;
using Application.DTOs.Outputs;
using Application.DTOs.Outputs.Auth;
using Application.DTOs.Outputs.User;

public interface IAuthService
{
    Task<OperationResultDTO<string>> RefreshAccessTokenAsync(string refreshToken);
    Task<OperationResultDTO<UserAuthOutputDTO>> ValidateUserCredentialsAsync(UserCredentialsInputDTO dto);
    Task<OperationResultDTO<UserAuthOutputDTO>> RefreshAccessTokenAsync(Guid refreshToken, string ipAddress, string deviceInfo);
    Task<OperationResultDTO<UserOutputDTO>> GetCurrentUserAsync(string userId);
}