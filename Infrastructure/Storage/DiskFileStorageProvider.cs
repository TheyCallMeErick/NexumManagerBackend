using Application.Adapters;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Storage;

class DiskFileStorageProvider(IConfiguration configuration) : IFileStorage
{
    private readonly string _storagePath = configuration.GetSection("storage").Value ?? "/tmp";

    public async Task<string> WriteFileAsync(Stream fileStream, string fileName)
    {
        Directory.CreateDirectory(Path.GetDirectoryName(_storagePath) ?? "/tmp");
        string newFileName = Guid.NewGuid().ToString() + Path.GetExtension(fileName);
        string filePath = Path.Combine(_storagePath, newFileName);
        await using var fileOutput = new FileStream(filePath, FileMode.Create);
        await fileStream.CopyToAsync(fileOutput); return newFileName;
    }
    public async Task<Stream?> ReadFileAsync(string fileName)
    {
        string filePath = Path.Combine(_storagePath, fileName);
        if (!File.Exists(filePath))
        {
            return null;
        }
        return File.OpenRead(filePath);
    }
    public bool DeleteFileAsync(string path)
    {
        if (!File.Exists(path))
        {
            return false;
        }
        File.Delete(path);
        return true;
    }
}