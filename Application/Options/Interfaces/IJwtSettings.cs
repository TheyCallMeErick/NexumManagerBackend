namespace Application.Options.Interfaces;
public interface IJwtSettings
{
    string ExpirationTimeInMinutes {get;}
    string Issuer {get;}
    string Audience {get;}
    string SecretKey {get;}
}