using System.Security.Claims;
using Flare.DataAccess;
using Flare.DataAccess.Persistence;
using Flare.Application.Helpers;
using Flare.Application.Models.Account;
using Flare.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace Flare.Application.Services.Impl;

public class AccountService : IAccountService
{
    private readonly IUnitOfWork _unitOfWork;
	private readonly IConfiguration _configuration;
    private readonly IFileHandlingService _fileService;
    private readonly IHttpContextAccessor _accessor;

	public AccountService(IUnitOfWork unitOfWork, IConfiguration configuration, IFileHandlingService fileService, IHttpContextAccessor accessor)
	{
		_unitOfWork = unitOfWork;
		_configuration = configuration;
        _fileService = fileService;
        _accessor = accessor;
	}

	public async Task<CreateAccountResponseModel> CreateAccountAsync(CreateAccountModel createAccountModel)
	{
		PasswordHashing.CreatePasswordHash(createAccountModel.Password, out byte[] passwordHash, out byte[] passwordSalt);

		var account = new Account
		{
			Id = new Guid(),
			Email = createAccountModel.Email,
			Username = createAccountModel.Username,
			PasswordHash = passwordHash,
			PasswordSalt = passwordSalt,
			VerificationToken = TokenGeneration.GenerateRandomToken()
		};

		await _unitOfWork.Accounts.AddAsync(account);
        _fileService.CreateDirectory(Path.Combine("files", account.Username));

		return new CreateAccountResponseModel { Id = account.Id };
	}

	public async Task<LoginAccountResponseModel> LoginAsync(LoginAccountModel loginAccountModel)
	{
    	var account = await _unitOfWork.Accounts.GetAsync(x => x.Username == loginAccountModel.Username);
		bool isPasswordCorrect = PasswordHashing.VerifyPasswordHash(loginAccountModel.Password, account.PasswordHash, account.PasswordSalt);

		if (account == null) throw new Exception("Account not found");
		if (account.VerifiedAt == null) throw new Exception("Account not verified");
		if (isPasswordCorrect == false) throw new Exception("Password is incorrect");

		var token = JwtHelper.GenerateToken(account, _configuration);

		return new LoginAccountResponseModel
		{
			Username = account.Username,
			Email = account.Email,
			Token = token
		};
	}

	public async Task<ConfirmEmailResponseModel> ConfirmEmailAsync(ConfirmEmailModel confirmEmailModel)
	{
		var account = await _unitOfWork.Accounts.GetAsync(x => x.VerificationToken == confirmEmailModel.Token);

		if (account == null) throw new Exception("Your verification link is incorrect");

		account.VerifiedAt = DateTime.UtcNow;
    	await _unitOfWork.Accounts.UpdateAsync(account);
		return new ConfirmEmailResponseModel { Confirmed = true };
	}

	public async Task<ForgotPasswordResponseModel> ForgotPasswordAsync(ForgotPasswordModel forgotPasswordModel)
	{
		var account = await _unitOfWork.Accounts.GetAsync(x => x.Email == forgotPasswordModel.Email);

		if (account == null) throw new Exception("Account not found");

		account.ResetTokenExpires = DateTime.UtcNow.AddDays(3);
		account.PasswordResetToken = TokenGeneration.GenerateRandomToken();

		await _unitOfWork.Accounts.UpdateAsync(account);
		return new ForgotPasswordResponseModel { Id = account.Id};
	}

	public async Task<ResetPasswordResponseModel> ResetPasswordAsync(ResetPasswordModel resetPasswordModel)
	{
		var account = await _unitOfWork.Accounts.GetAsync(x => x.PasswordResetToken == resetPasswordModel.Token);

		if (account == null) throw new Exception("Account not found");
		if (account.ResetTokenExpires < DateTime.UtcNow) throw new Exception("Token expired or invalid");

		PasswordHashing.CreatePasswordHash(resetPasswordModel.Password, out byte[] passwordHash, out byte[] passwordSalt);

		account.PasswordHash = passwordHash;
		account.PasswordSalt = passwordSalt;
		account.PasswordResetToken = null;
		account.ResetTokenExpires = null;

		await _unitOfWork.Accounts.UpdateAsync(account);
		return new ResetPasswordResponseModel { Id = account.Id };
	}

    public async Task<DeleteAccountResponseModel> DeleteAccountAsync(DeleteAccountModel deleteAccountModel)
    {
        var account = await _unitOfWork.Accounts.GetAsync(x => x.Id == deleteAccountModel.Id);
        var username = _accessor.HttpContext?.User.FindFirstValue(ClaimTypes.Name);

        if (account == null) throw new Exception("Account not found");
        if (account.Username != username) throw new Exception("Only owner of account can delete it");

        await _unitOfWork.Accounts.RemoveAsync(account);
        _fileService.DeleteDirectory(Path.Combine("files", account.Username));

        return new DeleteAccountResponseModel { Id = account.Id };
    }
}