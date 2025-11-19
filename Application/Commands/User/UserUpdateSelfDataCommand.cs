using Application.Adapters;
using Application.DTOs.Inputs.User;
using Application.Services.Interfaces;
using Domain.Data.Repositories;

namespace Application.Commands.User;

public class UserUpdateSelfDataCommand(IUserRepository userRepository, IHashService hashService, IFileStorage fileStorageProvider)
{
    public async Task<bool> Execute(UpdateSelfDataDto dto)
    {
        var user = await userRepository.FindById(dto.Id);
        if (user == null)
        {
            return false;
        }

        user.Username = dto.Username ?? user.Username;
        user.Name = dto.Name ?? user.Name;
        user.Email = dto.Email ?? user.Email;
        if (dto.Password != null)
        {
            var passwordHash = hashService.HashPassword(dto.Password);
            user.PasswordHash = passwordHash ;
        }
        if(dto.ProfilePicture != null)
        {
           var profilePictureFileName = await fileStorageProvider.WriteFileAsync(
            dto.ProfilePicture.OpenReadStream(),
            dto.ProfilePicture.FileName
            );
            user.ProfilePictureFileName = profilePictureFileName;
        }
        user.Username = dto.Username ?? user.Username;
        user.EnableNotifications = dto.EnableNotifications;
        await userRepository.Update(user);
        return true;
    }
}
