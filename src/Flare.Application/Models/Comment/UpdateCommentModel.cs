using System.ComponentModel.DataAnnotations;

namespace Flare.Application.Models.Comment;

public class UpdateCommentModel
{
    [Required]
    public Guid Id { get; set; }

    [Required, MaxLength(500)]
    public string Text { get; set; } = String.Empty;
}

public class UpdateCommentResponseModel : BaseResponseModel {}