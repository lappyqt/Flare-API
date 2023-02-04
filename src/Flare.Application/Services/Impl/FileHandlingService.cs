using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp.Formats.Jpeg;

namespace Flare.Application.Services.Impl;

public class FileHandlingService : IFileHandlingService
{
    private readonly IWebHostEnvironment _environment;
    private readonly string _filesDirectory;

    public FileHandlingService(IWebHostEnvironment environment)
    {
        _environment = environment;
        _filesDirectory = _environment.WebRootPath;
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
        string fullPath = Path.Combine(_filesDirectory, path);

        if (file.Length > 0)
        {
            await using var stream = new FileStream(fullPath, FileMode.Create);
            await file.CopyToAsync(stream);
        }
    }
}