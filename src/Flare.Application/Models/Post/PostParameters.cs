using Flare.Domain.Enums;

namespace Flare.Application.Models.Post;

public class PostParameters : PaginationParameters
{
    public ContentType? Type { get; set; } = null;
    public string? Category { get; set; } = null;
}