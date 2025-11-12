using Domain.Models;

namespace Infrastructure.DTOs; 

public record RegisterUserDTO(string Email, string Password);