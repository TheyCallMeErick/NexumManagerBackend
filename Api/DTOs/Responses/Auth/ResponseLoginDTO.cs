using System.Text.Json.Serialization;

namespace Api.DTOs.Responses.Auth; 

public record ResponseLoginDTO(
    [property: JsonPropertyName("access_token")]
    string AccessToken,
    [property: JsonPropertyName("refresh_token")]
    string RefreshToken
);