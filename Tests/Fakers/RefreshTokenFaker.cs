using Bogus;
using Bogus.DataSets;
using Domain.Models;

namespace Tests.Fakers; 

public class RefreshTokenFaker
{
    public static Faker<RefreshToken> Make()
    {
        return new Faker<RefreshToken>()
            .RuleFor(p => p.Id, f => Guid.NewGuid())
            .RuleFor(p => p.CreatedAt, f => new Date().Past().ToUniversalTime())
            .RuleFor(p => p.ExpiresAt, f => new Date().Future().ToUniversalTime())
            .RuleFor(p => p.Token, f => string.Join("-", new Lorem().Words(3)))
            .RuleFor(p => p.IsRevoked, f => f.Random.Bool())
            .RuleFor(p => p.RevokedAt, f => new Date().Past().ToUniversalTime())
            .RuleFor(p => p.CreatedByIp, f => new Internet().Ip())
            .RuleFor(p => p.DeviceInfo, f => new Lorem().Sentence(2, 5));
    }
}
