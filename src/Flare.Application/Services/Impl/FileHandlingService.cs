using Flare.Domain.Entities;
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

    private const int FullscreenWidth = 900;
    private const int ThumbnailWidth = 480;

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
            Directory.Delete(fullPath, true);
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

    public async Task UploadImageAsync(IFormFile file, Urls urls)
    {
        if (file.Length > 0)
        {
            var image = await Image.LoadAsync(file.OpenReadStream());

            await Process(image, Path.Combine(_filesDirectory, urls.Original), image.Width);
            await Process(image, Path.Combine(_filesDirectory, urls.Fullscreen), FullscreenWidth);
            await Process(image, Path.Combine(_filesDirectory, urls.Thumbnail), ThumbnailWidth);
        }
    }

    private async Task Process(Image image, string path, int resizeWidth)
    {
        var width = image.Width;
        var height = image.Height;

        image.Metadata.ExifProfile = null;

        if (image.Width > resizeWidth)
        {
            height = (int) ((double) resizeWidth / width * height);
            width = resizeWidth;
        }

        image.Mutate(x => x.Resize(width, height));

        await image.SaveAsJpegAsync(Path.Combine(path), new JpegEncoder
        {
            Quality = 80
        });
    }
}