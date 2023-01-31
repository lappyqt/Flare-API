using Microsoft.AspNetCore.Http;

namespace Flare.Application.Services;

public interface IFileHandlingService
{
    void CreateDirectory(string path);
    void DeleteDirectory(string path);
    void DeleteFile(string path);
    Task UploadFileAsync(IFormFile file, string path);
}