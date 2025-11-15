using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Api.DTOs.Requests.Auth; 

public record LoginRequestDTO(
    [property: JsonPropertyName("email")]
    [property: EmailAddress]
    string Email,
    [property: JsonPropertyName("password")]
    string Password
);