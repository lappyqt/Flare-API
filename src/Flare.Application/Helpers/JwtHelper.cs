using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Flare.Domain.Entities;

namespace Flare.Application.Helpers;

public static class JwtHelper
{
	public static string GenerateToken(Account account, IConfiguration configuration)
	{
		var secretKey = configuration.GetValue<string>("JwtConfiguration:SecretKey");
		var tokenHandler = new JwtSecurityTokenHandler();

		var tokenDescriptor = new SecurityTokenDescriptor
		{
			Subject = new ClaimsIdentity(new[]
			{
				new Claim(ClaimTypes.NameIdentifier, account.Id.ToString()),
				new Claim(ClaimTypes.Name, account.Username),
				new Claim(ClaimTypes.Email, account.Email)
			}),

			Expires = DateTime.UtcNow.AddDays(AuthOptions.Lifetime),
			SigningCredentials =
				new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(secretKey), SecurityAlgorithms.HmacSha256Signature)
		};

		var token = tokenHandler.CreateToken(tokenDescriptor);
		return tokenHandler.WriteToken(token);
	}
}