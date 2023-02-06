using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

namespace Flare.DataAccess;

public static class DataAccessDependencyInjection
{
    public static IServiceCollection AddDataAccessLayer(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDatabase(configuration);
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }

    private static void AddDatabase(this IServiceCollection services, IConfiguration configuration) {
        var host = configuration["PGHOST"];
        var port = configuration["PGPORT"];
        var database = configuration["PGDATABASE"];
        var username = configuration["PGUSER"];
        var password = configuration["PGPASSWORD"];

        services.AddDbContext<DatabaseContext>(options =>
            options.UseNpgsql($"Host = {host}; Port = {port}; Database = {database}; Username = {username}; Password = {password};"));
    }
}