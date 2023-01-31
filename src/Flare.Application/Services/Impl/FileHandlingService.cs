using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace Flare.Application.Services.Impl;

public class FileHandlingService : IFileHandlingService
{
    private readonly IWebHostEnvironment _environment;
    private readonly string _filesDirectory;

    public FileHandlingService(IWebHostEnvironment environment)
    {
        _environment = environment;
        _filesDirectory = Path.Combine(_environment.WebRootPath, "files");
    }

    public void CreateDirectory(string path)
    {
        Directory.CreateDirectory(Path.Combine(_filesDirectory, path));
    }

    public void DeleteDirectory(string path)
    {
        string fullPath = Path.Combine(_filesDirectory, path);

        if (Directory.Exists(fullPath))
        {
            Directory.Delete(fullPath);
        }
    }

    public void DeleteFile(string path)
    {
        string fullPath = Path.Combine(_filesDirectory, path);

        if (File.Exists(fullPath))
        {
            File.Delete(fullPath);
        }
    }

    public async Task UploadFileAsync(IFormFile file, string path)
    {
        string fullPath = Path.Combine(_environment.ContentRootPath, path);

        if (file.Length > 0)
        {
            await using var stream = new FileStream(fullPath, FileMode.Create);
            await stream.CopyToAsync(stream);
        }
    }
}