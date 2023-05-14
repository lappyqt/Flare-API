using System.ComponentModel.DataAnnotations;
using Flare.Application.Models.DataAnnotations;
using Flare.Domain.Enums;
using Microsoft.AspNetCore.Http;

namespace Flare.Application.Models.Post;

public class CreatePostModel
{
    [Required, MaxLength(500)]
    public string Description { get; set; } = string.Empty;

    [Required, ImageFileSignature, MaxFileSize(20 * 1024 * 1024)]
    public IFormFile? Content { get; set; }

    [Required]
    public Orientation Orientation { get; set; }

    [Required]
    public ContentType Type { get; set; }

    [Required]
    public string Category { get; set; } = string.Empty;

    [MaxLength(10)]
    public List<string>? Tags { get; set; } = new List<string>();
}

public class CreatePostResponseModel : BaseResponseModel {}