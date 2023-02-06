using Microsoft.EntityFrameworkCore;

namespace Flare.Domain.Entities;

public class Post : BaseEntity, IAuditedEntity
{
    public string Description { get; set; } = string.Empty;
    public Urls Urls { get; set; } = new Urls();
    public Orientation Orientation { get; set; }
    public ContentType Type { get; set; }
    public string Category { get; set; } = string.Empty;
    public List<string>? Tags { get; set; } = new List<string>();
    public ICollection<Comment>? Comments { get; set; }
    public int Views { get; set; }
    public int Downloads { get; set; }
    public string CreatedBy { get; set; } = string.Empty;
    public DateTime CreatedOn { get; set; }
    public DateTime? UpdatedOn { get; set; }
}

[Owned]
public class Urls
{
    public string Original { get; set; } = string.Empty;
    public string Fullscreen { get; set; } = string.Empty;
    public string Thumbnail { get; set; } = string.Empty;
}