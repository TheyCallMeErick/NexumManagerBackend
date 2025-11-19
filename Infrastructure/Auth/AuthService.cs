using Application.DTOs.Inputs.Auth;
using Application.DTOs.Outputs;
using Application.DTOs.Outputs.Auth;
using Application.DTOs.Outputs.User;
using Application.Services.Interfaces;
using Domain.Models;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Auth; 

public class AuthService(ApplicationDbContext context, ITokenManagerService tokenManager) : IAuthService
{
    public async Task<OperationResultDto<string>> RefreshAccessTokenAsync(string refreshToken)
    {
        var result = await tokenManager.RefreshAccessToken(refreshToken);
        if (!result.Success)
        {
            return OperationResultDto<string>.FailureResult(result.Message ?? "Failed to refresh token.");
        }
        return OperationResultDto<string>
            .SuccessResult()
            .WithData(result.Data);
    }

    public async Task<OperationResultDto<UserAuthOutputDto>> ValidateUserCredentialsAsync(UserCredentialsInputDto dto)
    {
        var user = await context.Users.FirstOrDefaultAsync(x => x.Email == dto.Email);
        if (user == null)
        {
            return OperationResultDto<UserAuthOutputDto>
                .FailureResult("Invalid email or password.");
        }

        var isPasswordValid = BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash);
        if (!isPasswordValid)
        {
            return OperationResultDto<UserAuthOutputDto>
                .FailureResult("Invalid email or password.");
        }

        var accessToken = tokenManager.GenerateAccessToken(user);
        var refreshToken = new RefreshToken
        {
            UserId = user.Id,
            Token = Guid.NewGuid().ToString(),
            ExpiresAt = DateTime.UtcNow.AddDays(30),
            CreatedByIp = dto.Ip,
            DeviceInfo = dto.DeviceInfo
        };

        context.RefreshTokens.Add(refreshToken);
        await context.SaveChangesAsync();

        return OperationResultDto<UserAuthOutputDto>
            .SuccessResult()
            .WithData(new UserAuthOutputDto
            (
                AccessToken: accessToken,
                RefreshToken: refreshToken.Token
            ));
    }

    public async Task<OperationResultDto<UserAuthOutputDto>> RefreshAccessTokenAsync(Guid refreshToken, string ipAddress, string deviceInfo)
    {
        var result = await tokenManager.RenewRefreshToken(refreshToken, ipAddress, deviceInfo);
        if (!result.Success)
        {
            return OperationResultDto<UserAuthOutputDto>.FailureResult(result.Message ?? "Failed to refresh token.");
        }
        return OperationResultDto<UserAuthOutputDto>
            .SuccessResult()
            .WithData(result.Data);
    }

    public async Task<OperationResultDto<UserOutputDto>> GetCurrentUserAsync(string userId)
    {
        var result = await context.Users.FirstOrDefaultAsync(u => u.Id.ToString() == userId);
        if (result == null)
        {
            return OperationResultDto<UserOutputDto>.FailureResult("Failed to get user from token.");
        }
        return OperationResultDto<UserOutputDto>
            .SuccessResult()
            .WithData(new UserOutputDto
            (
                Id : result.Id,
                Username : result.Username,
                Name : result.Name,
                Email : result.Email,
                ProfilePictureFileName : result.ProfilePictureFileName
            ));
    }
}