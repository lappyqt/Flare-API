namespace Flare.Domain.Entities;

public class Comment : BaseEntity, IAuditedEntity
{
    public Guid PostId { get; set; } 
    public string Text { get; set; } = string.Empty;
    public string CreatedBy { get; set; } = string.Empty;
    public DateTime CreatedOn { get; set; }
    public DateTime? UpdatedOn { get; set; }   
}