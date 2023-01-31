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
        services.AddDbContext<DatabaseContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("Database")));
    }
}