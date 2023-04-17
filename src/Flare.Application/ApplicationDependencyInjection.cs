using Flare.Application.Profiles;
using Flare.Application.Services;
using Flare.Application.Services.Impl;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Flare.Application;

public static class ApplicationDependencyInjection
{
    public static IServiceCollection AddApplicationLayer(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMapper();
        services.AddFluentEmail();
        services.AddServices();
        return services;
    }

    private static void AddMapper(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(IMappingProfiles));
    }

    private static void AddFluentEmail(this IServiceCollection services)
    {
        var smtpHost = Environment.GetEnvironmentVariable("SMTP_HOST");
        var smtpUsername = Environment.GetEnvironmentVariable("SMTP_USERNAME");
        var smtpPassword = Environment.GetEnvironmentVariable("SMTP_PASSWORD");

        services
            .AddFluentEmail(smtpUsername)
            .AddSmtpSender(smtpHost, 587, smtpUsername, smtpPassword);
    }

    private static void AddServices(this IServiceCollection services)
    {
        services.AddSingleton<IFileHandlingService, FileHandlingService>();
        services.AddScoped<IAccountService, AccountService>();
        services.AddScoped<IPostService, PostService>();
        services.AddScoped<ICommentService, CommentService>();
        services.AddScoped<ICategoryService, CategoryService>();
    }
}