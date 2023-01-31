namespace Flare.Domain.Entities;

public class Comment : BaseEntity, IAuditedEntity
{
    public string Text { get; set; } = string.Empty;
    public string CreatedBy { get; set; } = string.Empty;
    public Post Post { get; set; }
    public DateTime CreatedOn { get; set; }
    public DateTime? UpdatedOn { get; set; }   
}