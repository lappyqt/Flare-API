using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Flare.Application;

public class AuthOptions
{
    public const string Issuer = "FlareAuthenticationServer";
    public const string Audience = "FlareAuthenticationClient";
    public const int Lifetime = 7;

    public static SymmetricSecurityKey GetSymmetricSecurityKey(string key)
    {
        return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key));
    }
}