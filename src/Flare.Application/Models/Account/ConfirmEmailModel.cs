using System.ComponentModel.DataAnnotations;

namespace Flare.Application.Models.Account;

public class ConfirmEmailModel
{
	[Required]
	public string Token { get; set; } = String.Empty;
}

public class ConfirmEmailResponseModel
{
	public bool Confirmed { get; set; }
}