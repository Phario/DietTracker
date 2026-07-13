using DietTracker.Core.Common.Abstractions;
using DietTracker.Core.Common.Internal;
using Microsoft.Extensions.DependencyInjection;

namespace DietTracker.Core.Common;

public static class ServiceExtensions
{
    public static IServiceCollection AddCommonServices(this IServiceCollection services)
    {
        services.AddScoped<IAuthorizedUserProvider, AuthorizedUserProvider>();
        
        return services;
    }
}