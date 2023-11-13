using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using sample_crm.Application.Mapping;
namespace sample_crm.Application;

public static class ApplicationDependencyInjection
{
    // extension
    public static IServiceCollection AddApplicationLayer(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddServices();
        services.RegisterAutoMapper();
        return services;
    }

    // extension
    private static void AddServices(this IServiceCollection services)
    {

    }

    // extension
    private static void RegisterAutoMapper(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(AutoMapperMarker));
    }
}
