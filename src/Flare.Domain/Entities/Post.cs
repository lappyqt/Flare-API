namespace Flare.Domain.Entities;

public class Post : BaseEntity, IAuditedEntity
{
    public string Description { get; set; } = string.Empty;
    public string ContentPath { get; set; } = string.Empty;
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