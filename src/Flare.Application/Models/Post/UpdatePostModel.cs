using System.ComponentModel.DataAnnotations;

namespace Flare.Application.Models.Post;

public class UpdatePostModel
{
    [Required]
    public Guid Id { get; set; }

    [MaxLength(500)]
    public string Description { get; set; } = String.Empty;

    [MaxLength(10)]
    public List<string> Tags { get; set; } = new List<string>();
}

public class UpdatePostResponseModel : BaseResponseModel {}