using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Wolfpack.Business.Interface;
using Wolfpack.Business.Services;

namespace Wolfpack.Business;

public static class BusinessStartupExtensions
{
    public static IServiceCollection AddBusiness(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IPackService, PackService>();
        services.AddScoped<IWolfService, WolfService>();

        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

        return services;
    }
}