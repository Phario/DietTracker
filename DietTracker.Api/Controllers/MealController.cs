using DietTracker.Core.Meals.Commands;
using DietTracker.Core.Meals.Queries;
using DietTracker.Core.Meals.Results;
using DietTracker.Requests;
using DispatchR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace DietTracker.Controllers;

internal static class MealController
{
    public static IEndpointRouteBuilder MapMealEndpoints(this IEndpointRouteBuilder endpoints)
    {
        var group = endpoints.MapGroup("meals");

        group.WithTags("Stores");
        
        group.MapGet("all", GetAllMealsAsync);
        
        group.MapPost("create", CreateMealAsync);
        
        return endpoints;
    }

    private static async Task<Ok<Guid>> CreateMealAsync([FromServices] IMediator mediator,
        [FromBody] CreateMealRequest request, CancellationToken ct)
    {
        var mealId = await mediator.Send(new CreateMealCommand
        {
            Name = request.Name,
            Calories = request.Calories,
            Carbs = request.Carbs,
            Fats = request.Fats,
            Protein = request.Protein,
        }, ct);
        
        return TypedResults.Ok(mealId);
    }

    private static async Task<Ok<IEnumerable<MealDto>>> GetAllMealsAsync([FromServices] IMediator mediator,
        CancellationToken ct)
    {
        var stores = await mediator.Send(new GetAllMealsQuery(), ct);
        
        return TypedResults.Ok(stores);
    }
}