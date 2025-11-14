using Application.Adapters;
using Application.DTOs.Inputs.User;
using Application.Services.Interfaces;
using Domain.Data.Repositories;

namespace Application.Commands.User;

public class UserUpdateSelfDataCommand
{
    private readonly IUserRepository _userRepository;
    private readonly IHashService _hashService;
    private readonly IFileStorage _fileStorageProvider;

    public UserUpdateSelfDataCommand(IUserRepository userRepository, IHashService hashService, IFileStorage fileStorageProvider)
    {
        _userRepository = userRepository;
        _hashService = hashService;
        _fileStorageProvider = fileStorageProvider;
    }

    public async Task<bool> Execute(UpdateSelfDataDTO dto)
    {
        var user = await _userRepository.FindById(dto.Id);
        if (user == null)
        {
            return false;
        }

        user.Username = dto.Username ?? user.Username;
        user.Name = dto.Name ?? user.Name;
        user.Email = dto.Email ?? user.Email;
        if (dto.Password != null)
        {
            var PasswordHash = _hashService.HashPassword(dto.Password);
            user.PasswordHash = PasswordHash ;
        }
        if(dto.ProfilePicture != null)
        {
           var ProfilePictureFileName = await _fileStorageProvider.WriteFileAsync(
            dto.ProfilePicture.OpenReadStream(),
            dto.ProfilePicture.FileName
            );
            user.ProfilePictureFileName = ProfilePictureFileName;
        }
        user.Username = dto.Username ?? user.Username;
        user.EnableNotifications = dto.EnableNotifications;
        await _userRepository.Update(user);
        return true;
    }
}
