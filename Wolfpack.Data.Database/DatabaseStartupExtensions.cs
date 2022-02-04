using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Wolfpack.Data.Database;

public static class DatabaseStartupExtensions
{
    public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<WolfpackContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("Database")));

        return services;
    }

    public static IApplicationBuilder MigrateDatabase(this IApplicationBuilder builder)
    {
        using var scope = builder.ApplicationServices.CreateScope();
        using var context = scope.ServiceProvider.GetRequiredService<WolfpackContext>();

        context.Database.Migrate();

        return builder;
    }
}