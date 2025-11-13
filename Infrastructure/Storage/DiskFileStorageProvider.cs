using Application.Adapters;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Storage;

class DiskFileStorageProvider : IFileStorage
{
    private readonly string storagePath;
    public DiskFileStorageProvider(IConfiguration configuration)
    {
        storagePath = configuration.GetSection("storage").Value ?? "/tmp";
    }

    public async Task<string> WriteFileAsync(Stream fileStream, string fileName)
    {
        Directory.CreateDirectory(Path.GetDirectoryName(storagePath) ?? "/tmp");
        string newFileName = Guid.NewGuid().ToString() + Path.GetExtension(fileName);
        string filePath = Path.Combine(storagePath, newFileName);
        await using var fileOutput = new FileStream(filePath, FileMode.Create);
        await fileStream.CopyToAsync(fileOutput); return newFileName;
    }
    public async Task<Stream?> ReadFileAsync(string fileName)
    {
        string filePath = Path.Combine(storagePath, fileName);
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