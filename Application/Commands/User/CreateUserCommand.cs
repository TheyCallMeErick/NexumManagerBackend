using Application.DTOs.Inputs.User;
using Application.Services.Interfaces;
using Domain.Data.Repositories;

namespace Application.Commands.User; 

public class CreateUserCommand(IUserRepository userRepository, IHashService hashService)
{
    public async Task<bool> Execute(CreateUserDto dto)
    {
        var password = hashService.HashPassword(dto.Password);
        var user = new Domain.Models.User
        {
            Username = dto.Username,
            Email = dto.Email,
            PasswordHash = password,
        };
        await userRepository.Create(user);
        return true;
    }
}
