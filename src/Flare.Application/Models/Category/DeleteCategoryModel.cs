using System.ComponentModel.DataAnnotations;

namespace Flare.Application.Models.Category;

public class DeleteCategoryModel
{
    [Required]
    public Guid Id { get; set; }
}

public class DeleteCategoryResponseModel : BaseResponseModel {}