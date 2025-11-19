using Application.DTOs.Inputs.Auth;
using Application.Services.Interfaces;
using Infrastructure.Auth;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Tests.Fakers;

namespace Tests.Services; 

public class AuthServiceTests : IDisposable
{
    private readonly IAuthService _authService;
    private readonly ApplicationDbContext _dbContext;
    public AuthServiceTests()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
        var configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(new Dictionary<string, string?>
            {
                { "JwtSettings:SecretKey", Guid.NewGuid().ToString() },
                { "JwtSettings:ExpirationTimeInMinutes", "60" },
                { "JwtSettings:Issuer", "issuer" },
                { "JwtSettings:Audience", "audience" }
            })
            .Build();
        _dbContext = new ApplicationDbContext(options);
        var tokenManagerService = new TokenManagerService(configuration, _dbContext);

        _authService = new AuthService(_dbContext, tokenManagerService);
    }

    [Fact]
    public async Task AuthenticateUser_ShouldReturnValidToken()
    {
        // Arrange
        var user = UserFaker.Make().Generate();
        user.PasswordHash = BCrypt.Net.BCrypt.HashPassword("TestPassword");
        _dbContext.Users.Add(user);
        await _dbContext.SaveChangesAsync();

        // Act
        var result = await _authService.ValidateUserCredentialsAsync(new UserCredentialsInputDto
        (
            Email : user.Email!,
            Password : "TestPassword",
            Ip : "",
            DeviceInfo : "TestDevice"
        ));

        // Assert
        Assert.True(result.Success);
        Assert.NotNull(result.Data);
        Assert.NotEmpty(result.Data.AccessToken);
    }

    [Fact]
    public async Task RefreshAccessToken_ShouldReturnNewAccessToken()
    {
        // Arrange
        var user = UserFaker.Make().Generate();
        user.PasswordHash = BCrypt.Net.BCrypt.HashPassword("TestPassword");
        _dbContext.Users.Add(user);
        await _dbContext.SaveChangesAsync();

        var authResult = await _authService.ValidateUserCredentialsAsync(new UserCredentialsInputDto
        (
            Email : user.Email!,
            Password : "TestPassword",
            Ip : "",
            DeviceInfo : "TestDevice"
        ));

        Assert.True(authResult.Success);
        Assert.NotNull(authResult.Data);
        Assert.NotEmpty(authResult.Data.AccessToken);

        // Act
        var refreshResult = await _authService.RefreshAccessTokenAsync(authResult.Data.RefreshToken);

        // Assert
        Assert.True(refreshResult.Success);
        Assert.NotNull(refreshResult.Data);
        Assert.NotEmpty(refreshResult.Data);
    }

    [Fact]
    public async Task RefreshAccessToken_WithInvalidToken_ShouldReturnFailure()
    {
        // Arrange
        var invalidToken = Guid.NewGuid().ToString();

        // Act
        var result = await _authService.RefreshAccessTokenAsync(invalidToken);

        // Assert
        Assert.False(result.Success);
        Assert.Equal("Invalid refresh token.", result.Message);
    }

    [Fact]
    public async Task ValidateUserCredentials_WithInvalidCredentials_ShouldReturnFailure()
    {
        // Arrange 
        var user = UserFaker.Make().Generate();
        user.PasswordHash = BCrypt.Net.BCrypt.HashPassword("TestPassword");
        _dbContext.Users.Add(user);
        await _dbContext.SaveChangesAsync();

        // Act
        var result = await _authService.ValidateUserCredentialsAsync(new UserCredentialsInputDto
        (
            Email: user.Email!,
            Password: "WrongPassword",
            Ip: "",
            DeviceInfo: "TestDevice"
        ));

        // Assert
        Assert.False(result.Success);
        Assert.Equal("Invalid email or password.", result.Message);
        Assert.Null(result.Data);
    }

    public void Dispose()
    {
        _dbContext.Database.EnsureDeleted();
        _dbContext.Dispose();
    }
}