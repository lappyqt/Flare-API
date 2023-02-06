using Flare.Application.Models.Account;
using Flare.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Flare.API.Controllers;

[ApiController]
[Route("[controller]")]
[AllowAnonymous]
public class AccountsController : ControllerBase
{
    private readonly IAccountService _service;

    public AccountsController(IAccountService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<IActionResult> CreateAccountAsync(CreateAccountModel createAccountModel)
    {
        return Ok(await _service.CreateAccountAsync(createAccountModel));
    }

    [HttpPost("login")]
    public async Task<IActionResult> LoginAsync(LoginAccountModel loginAccountModel)
    {
        return Ok(await _service.LoginAsync(loginAccountModel));
    }

    [HttpPost("confirm")]
    public async Task<IActionResult> ConfirmEmailAsync(ConfirmEmailModel confirmEmailModel)
    {
        return Ok(await _service.ConfirmEmailAsync(confirmEmailModel));
    }

    [HttpPost("forgot-password")]
    public async Task<IActionResult> ForgotPasswordAsync(ForgotPasswordModel forgotPasswordModel)
    {
        return Ok(await _service.ForgotPasswordAsync(forgotPasswordModel));
    }

    [HttpPost("reset-password")]
    public async Task<IActionResult> ResetPasswordAsync(ResetPasswordModel resetPasswordModel)
    {
        return Ok(await _service.ResetPasswordAsync(resetPasswordModel));
    }

    [HttpDelete]
    [Authorize]
    public async Task<IActionResult> DeleteAccountAsync(DeleteAccountModel deleteAccountModel)
    {
        return Ok(await _service.DeleteAccountAsync(deleteAccountModel));
    }
}