using Flare.Application.Services;
using Flare.Application.Services.Impl;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Flare.Application;

public static class ApplicationDependencyInjection
{
    public static IServiceCollection AddApplicationLayer(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddServices();
        return services;
    }

    private static void AddServices(this IServiceCollection services)
    {
        services.AddScoped<IFileHandlingService, FileHandlingService>();
        services.AddScoped<IAccountService, AccountService>();
    }
}