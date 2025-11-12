using Application.DTOs.Outputs;
using Application.DTOs.Outputs.Auth;
using Domain.Models;

namespace Application.Services.Interfaces;


public interface ITokenManagerService
{
    string GenerateAccessToken(User user);
    Task<OperationResultDTO<UserAuthOutputDTO>> RenewRefreshToken(Guid userId, string ipAddress, string deviceInfo);

    Task<OperationResultDTO<string>> RefreshAccessToken(string token);
}