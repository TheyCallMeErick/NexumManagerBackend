using Application.DTOs.Inputs.User;
using Application.Services.Interfaces;
using Domain.Data.Repositories;

namespace Application.Commands.User; 

public class CreateUserCommand
{
    private readonly IUserRepository _userRepository;
    private readonly IHashService _hashService;

    public CreateUserCommand(IUserRepository userRepository, IHashService hashService)
    {
        _userRepository = userRepository;
        _hashService = hashService;
    }

    public async Task<bool> Execute(CreateUserDTO dto)
    {
        var password = _hashService.HashPassword(dto.Password);
        var user = new Domain.Models.User
        {
            Username = dto.Username,
            Email = dto.Email,
            PasswordHash = password,
        };
        await _userRepository.Create(user);
        return true;
    }
}
