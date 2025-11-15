using System.Security.Claims;
using Api.DTOs.Requests.Auth;
using Api.DTOs.Responses.Auth;
using Application.Commands.Auth;
using Application.DTOs.Inputs.Auth;
using Application.Queries.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/v1/auth")]
[Authorize]
public class AuthController : ControllerBase
{

    [HttpGet("check")]
    public IActionResult Check()
    {
        return Ok();
    }

    [HttpPost("login")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(ResponseLoginDTO), 200)]
    public async Task<IActionResult> Login([FromBody] LoginRequestDTO loginRequest, [FromServices] ValidateUserCredentialsCommand command)
    {
        var result = await command.Execute(new UserCredentialsInputDTO
        (
            email: loginRequest.Email,
            password: loginRequest.Password,
            ip: HttpContext.Connection.RemoteIpAddress?.ToString() ?? string.Empty,
            deviceInfo: HttpContext.Request.Headers["User-Agent"].ToString()
        ));
        if (!result.Success)
        {
            return BadRequest(result.Message);
        }
        return Ok(new ResponseLoginDTO
        (
            AccessToken: result.Data!.AccessToken,
            RefreshToken: result.Data.RefreshToken
        ));
    }

    [HttpGet("refresh-access-token")]
    [AllowAnonymous]
    public async Task<IActionResult> RefreshAccessToken([FromServices] RefreshAccessTokenCommand command)
    {
        var refreshToken = HttpContext.Request.Headers["Refresh-Token"].ToString();
        if (string.IsNullOrEmpty(refreshToken))
        {
            return BadRequest("Refresh token is required.");
        }
        var result = await command.Execute(refreshToken);
        if (!result.Success)
        {
            return BadRequest(result.Message);
        }
        return Ok(result.Data);
    }

    [HttpGet("renew-tokens")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(ResponseLoginDTO), 200)]
    public async Task<IActionResult> RenewTokens([FromServices] RenewRefreshTokenCommand command)
    {
        var refreshToken = HttpContext.Request.Headers["Refresh-Token"].ToString();
        if (string.IsNullOrEmpty(refreshToken))
        {
            return BadRequest("Refresh token is required.");
        }
        var result = await command.Execute(
            new RenewRefreshTokenInputDTO(
                refreshToken: Guid.Parse(refreshToken),
                deviceInfo: HttpContext.Connection.RemoteIpAddress?.ToString() ?? string.Empty,
                ipAddress: HttpContext.Request.Headers["User-Agent"].ToString()
            )
        );
        if (!result.Success)
        {
            return BadRequest(result.Message);
        }
        return Ok(new ResponseLoginDTO
        (
            AccessToken: result.Data!.AccessToken,
            RefreshToken: result.Data.RefreshToken
        ));
    }

    [HttpGet("current-user")]
    public async Task<IActionResult> GetCurrentUser([FromServices] QueryUserById query)
    {
        var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        Console.WriteLine(userId);
        if (string.IsNullOrEmpty(userId))
        {
            return Unauthorized("User ID not found in token.");
        }
        if (!Guid.TryParse(userId, out var userIdGuid))
        {
            return BadRequest("Couldn't read current user");
        }
        var result = await query.Execute(userIdGuid);
        return Ok(result);
    }
}
