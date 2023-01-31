using System.Security.Cryptography;

namespace Flare.Application.Helpers;

public static class TokenGeneration
{
	public static string GenerateRandomToken() => Convert.ToHexString(RandomNumberGenerator.GetBytes(64));
}