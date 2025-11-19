using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Application.DTOs.Outputs;
using Application.DTOs.Outputs.Auth;
using Application.Services.Interfaces;
using Domain.Models;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.Auth; 

public class TokenManagerService(IConfiguration configuration, ApplicationDbContext context) : ITokenManagerService
{

    public string GenerateAccessToken(User user)
    {
        var jwtSettings = configuration.GetSection("JwtSettings");
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["SecretKey"] ?? string.Empty));
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.Username!),
            new Claim(ClaimTypes.Email, user.Email!),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString())
        };

        var expirationTime = DateTime.UtcNow.AddMinutes(Convert.ToDouble(jwtSettings["ExpirationTimeInMinutes"] ?? string.Empty));
        var issuer = configuration.GetSection("JwtSettings")["Issuer"] ?? string.Empty;
        var audience = configuration.GetSection("JwtSettings")["Audience"] ?? string.Empty;


        var token = new JwtSecurityToken(
            issuer: issuer,
            audience: audience,
            claims: claims,
            expires: expirationTime,
            signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public async Task<OperationResultDto<UserAuthOutputDto>> RenewRefreshToken(Guid userId, string ipAddress, string deviceInfo)
    {
        var oldRefreshToken = await context.RefreshTokens
            .FirstOrDefaultAsync(rt => rt.UserId == userId && !rt.IsRevoked && rt.ExpiresAt > DateTime.UtcNow);
        if (oldRefreshToken == null)
        {
            return OperationResultDto<UserAuthOutputDto>
                .FailureResult("No valid refresh token found for the user.");
        }
        if (oldRefreshToken.ReplacedByToken != null)
        {
            return OperationResultDto<UserAuthOutputDto>.FailureResult("This refresh token has already been used to generate a new refresh token.");
        }
        var user = oldRefreshToken.User;
        var newRefreshToken = new RefreshToken
        {
            UserId = userId,
            Token = Guid.NewGuid().ToString(),
            ExpiresAt = DateTime.UtcNow.AddDays(30),
            CreatedByIp = ipAddress,
            DeviceInfo = deviceInfo,
            CreatedAt = DateTime.UtcNow
        };
        oldRefreshToken.ReplacedByToken = newRefreshToken;
        oldRefreshToken.IsRevoked = true;
        context.RefreshTokens.Update(oldRefreshToken);
        context.RefreshTokens.Add(newRefreshToken);
        await context.SaveChangesAsync();
        var newAccessToken = GenerateAccessToken(user);

        return OperationResultDto<UserAuthOutputDto>
            .SuccessResult()
            .WithData(new UserAuthOutputDto
            (
                AccessToken: newAccessToken,
                RefreshToken: newRefreshToken.Token
            ));
    }

    public async Task<OperationResultDto<string>> RefreshAccessToken(string refreshToken)
    {
        if (string.IsNullOrWhiteSpace(refreshToken))
        {
            return OperationResultDto<string>.FailureResult("Token is null or empty.");
        }
        var validTokenResult = await context.RefreshTokens
            .AsNoTracking()
            .FirstOrDefaultAsync(rt => rt.Token == refreshToken);

        if (validTokenResult == null)
        {
            return OperationResultDto<string>.FailureResult("Invalid refresh token.");
        }

        if (validTokenResult.IsRevoked || validTokenResult.ReplacedByToken != null || validTokenResult.ExpiresAt < DateTime.UtcNow)
        {
            return OperationResultDto<string>.FailureResult("Invalid or expired refresh token.");
        }

        var user = await context.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.Id == validTokenResult.UserId);

        if (user == null)
        {
            return OperationResultDto<string>.FailureResult("User associated with the refresh token not found.");
        }

        var newAccessToken = GenerateAccessToken(user);

        return OperationResultDto<string>
            .SuccessResult()
            .WithData(newAccessToken);
    }
}