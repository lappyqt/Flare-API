using System.ComponentModel.DataAnnotations;

namespace Flare.Application.Models.Comment;

public class CreateCommentModel
{
    [Required]
    public Guid PostId { get; set; }

    [Required, MaxLength(500)]
    public string Text { get; set; } = String.Empty;
}

public class CreateCommentResponseModel : BaseResponseModel {}