using DietTracker.Persistence;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DietTracker.Core;

public static class ServiceExtensions
{
    public static IServiceCollection AddCoreServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddPersistence(configuration);
        
        return services;
    }
}