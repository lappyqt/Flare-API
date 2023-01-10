namespace Flare.Domain.Entities;

public class Category : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public ContentType Type { get; set; } 
}