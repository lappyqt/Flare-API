using System.ComponentModel.DataAnnotations;
using Flare.Domain.Enums;

namespace Flare.Application.Models.Category;

public class CreateCategoryModel
{
    [Required, MaxLength(50)]
    public string Name { get; set; } = String.Empty;
}

public class CreateCategoryResponseModel : BaseResponseModel
{
    public string Name { get; set; } = string.Empty;
}