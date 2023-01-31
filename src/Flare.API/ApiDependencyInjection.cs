using System.Text;
using Flare.Application;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace Flare.API;

public static class ApiDependencyInjection
{
    public static IServiceCollection AddApiLayer(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddJwt(configuration);
        return services;
    }

    private static void AddJwt(this IServiceCollection services, IConfiguration configuration)
    {
        var secretKey = configuration.GetValue<string>("JwtConfiguration:SecretKey");

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.SaveToken = true;
            options.RequireHttpsMetadata = false;

            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = AuthOptions.Issuer,
                ValidateAudience = true,
                ValidAudience = AuthOptions.Audience,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(secretKey)
            };
        });
    }
}