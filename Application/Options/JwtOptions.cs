using System.ComponentModel.DataAnnotations;

namespace Application.Options;

public record JwtOptions(
    [property: Required]
    string ExpirationTimeInMinutes,
    [property: Required]
    string Issuer,
    [property: Required]
    string Audience,
    [property: Required]
    string SecretKey);