using DietTracker.Controllers;

namespace DietTracker.Extensions;

internal static class EndpointRouteBuilderExtensions
{
    public static IEndpointRouteBuilder MapEndpoints(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapMealEndpoints();
        return endpoints;
    }
}