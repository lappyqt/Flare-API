using Flare.Domain.Enums;

namespace Flare.Application.Models.Post;

public class PostParameters
{
    public ContentType? Type { get; set; } = null;
    public string? Category { get; set; } = null;
    public int Page { get; set; } = 1;
    const int MaxPageSize = 30;

    private int _pageSize = 20;
    public int PageSize
    {
        get => _pageSize;
        set => _pageSize = (value > MaxPageSize) ? MaxPageSize : value;
    }
}