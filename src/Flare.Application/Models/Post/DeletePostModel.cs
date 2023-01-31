using System.ComponentModel.DataAnnotations;

namespace Flare.Application.Models.Post;

public class DeletePostModel
{
    [Required]
    public Guid Id { get; set; }
}

public class DeletePostResponseModel : BaseResponseModel {}