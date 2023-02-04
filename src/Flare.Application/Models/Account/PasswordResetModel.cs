using System.ComponentModel.DataAnnotations;

namespace Flare.Application.Models.Account;

public class ResetPasswordModel
{
	[Required]
	public string Token { get; set; } = String.Empty;

	[Required, MinLength(6)]
	public string Password { get; set; } = String.Empty;

	[Required, Compare(nameof(Password))]
	public string ConfirmPassword { get; set; } = String.Empty;
}

public class ResetPasswordResponseModel : BaseResponseModel {}