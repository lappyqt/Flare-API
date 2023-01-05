namespace Flare.Domain.Entities;

public class Post : BaseEntity, IAuditedEntity
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string ContentPath { get; set; } = string.Empty;
    public ContentType Type { get; set; }
    public string Category { get; set; } = string.Empty;
    public List<string>? Tags { get; set; } = new List<string>();
    public int Views { get; set; }
    public int Downloads { get; set; }
    public string CreatedBy { get; set; } = string.Empty;
    public DateTime CreatedOn { get; set; }
    public DateTime? UpdatedOn { get; set; }
}