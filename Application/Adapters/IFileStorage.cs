namespace Application.Adapters; 

public interface IFileStorage
{
    Task<string> WriteFileAsync(Stream fileStream, string fileName);
    Task<Stream?> ReadFileAsync(string path);
    bool DeleteFileAsync(string path);
}
