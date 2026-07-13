using DietTracker.Core.Common.Abstractions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DietTracker.Core.Common.Extensions;

public static class OptionsExtensions
{
    public static IServiceCollection AddOptionsWithRequiredFieldsValidation<T>(this IServiceCollection services,
        IConfiguration configuration) where T : class, IDietTrackerOptions
    {
        services.AddOptions<T>()
            .Bind(configuration.GetSection(T.SectionName))
            .ValidateOnStart()
            .ValidateDataAnnotations();

        return services;
    }
}