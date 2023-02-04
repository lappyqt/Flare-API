using Flare.Application.Models.Account;

namespace Flare.Application.Services;

public interface IAccountService
{
	Task<CreateAccountResponseModel> CreateAccountAsync(CreateAccountModel createAccountModel);
	Task<LoginAccountResponseModel> LoginAsync(LoginAccountModel loginAccountModel);
	Task<ConfirmEmailResponseModel> ConfirmEmailAsync(ConfirmEmailModel confirmEmailModel);
	Task<ForgotPasswordResponseModel> ForgotPasswordAsync(ForgotPasswordModel forgotPasswordModel);
	Task<ResetPasswordResponseModel> ResetPasswordAsync(ResetPasswordModel resetPasswordModel);
    Task<DeleteAccountResponseModel> DeleteAccountAsync(DeleteAccountModel deleteAccountModel);
}