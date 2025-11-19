using Application.Options;
using Application.Options.Interfaces;
using Microsoft.Extensions.Options;

namespace Infrastructure.Settings;
public sealed class JwtSettings(IOptions<JwtOptions> options) : IJwtSettings
{
    public string ExpirationTimeInMinutes => options.Value.ExpirationTimeInMinutes;
    public string Issuer => options.Value.Issuer;
    public string Audience => options.Value.Audience;
    public string SecretKey => options.Value.SecretKey;
}