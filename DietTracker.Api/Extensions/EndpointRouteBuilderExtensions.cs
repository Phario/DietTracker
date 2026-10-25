using DietTracker.Controllers;

namespace DietTracker.Extensions;

internal static class EndpointRouteBuilderExtensions
{
    public static IEndpointRouteBuilder MapEndpoints(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapMeals();
        endpoints.MapUsers();
        endpoints.MapWorkouts();
        return endpoints;
    }
}