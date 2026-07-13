using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using DietTracker.Core.Common.Extensions;
using DietTracker.Core.Auth.Settings;

namespace DietTracker.Core.Auth;

public static class ServiceExtensions
{
    public static IServiceCollection AddAuthServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddOptionsWithRequiredFieldsValidation<JwtSettings>(configuration);
        
        
        return services;
    }
}