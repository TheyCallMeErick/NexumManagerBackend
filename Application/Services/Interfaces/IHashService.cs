namespace Application.Services.Interfaces; 
public interface IHashService
{
    string HashPassword(string password);
    bool VerifyPassword(string hashedPassword, string password);

}