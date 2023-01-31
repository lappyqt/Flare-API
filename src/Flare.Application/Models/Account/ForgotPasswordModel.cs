using System.ComponentModel.DataAnnotations;

namespace Flare.Application.Models.Account;

public class ForgotPasswordModel
{
    [Required]
	public string Email { get; set; } = String.Empty;
}

public class ForgotPasswordResponseModel : BaseResponseModel {}