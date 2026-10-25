using DietTracker.Core.Workouts;
using DietTracker.Core.Workouts.Commands;
using DietTracker.Core.Workouts.Queries;
using DietTracker.Persistence.Enums;
using DietTracker.Requests;
using DispatchR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace DietTracker.Controllers;

internal static class WorkoutController
{
    public static IEndpointRouteBuilder MapWorkouts(this IEndpointRouteBuilder endpoints)
    {
        var group = endpoints.MapGroup("workouts");

        group.WithTags("Workouts");

        group.MapGet("all", GetAllWorkoutsAsync);
        group.MapGet("category/{bodyPart}", GetWorkoutsByBodyPartAsync);
        
        group.MapPost("create", CreateWorkoutAsync);
        
        return endpoints;
    }

    private static async Task<Ok<Guid>> CreateWorkoutAsync([FromServices] IMediator mediator,
        [FromBody] CreateWorkoutRoutineRequest request, CancellationToken ct)
    {
        var routineId = await mediator.Send(new CreateWorkoutRoutineCommand
        {
            Name = request.Name,
            Description = request.Description,
            TargetBodyPart = request.TargetBodyPart,
            DurationInMinutes = request.DurationInMinutes,
            Exercises = request.Exercises.Select(x => new ExerciseItemInput
            {
                Name = x.Name,
                Sets = x.Sets,
                Reps = x.Reps
            }).ToList()
        }, ct);
        
        return TypedResults.Ok(routineId);
    }

    private static async Task<Ok<IEnumerable<WorkoutRoutineDto>>> GetAllWorkoutsAsync([FromServices] IMediator mediator,
        CancellationToken ct)
    {
        var routines = await mediator.Send(new GetAllWorkoutRoutinesQuery(), ct);
        
        return TypedResults.Ok(routines);
    }
    
    private static async Task<Ok<IEnumerable<WorkoutRoutineDto>>> GetWorkoutsByBodyPartAsync(
        [FromServices] IMediator mediator,
        [FromRoute] BodyPart bodyPart,
        CancellationToken ct)
    {
        var routines = await mediator.Send(new GetWorkoutsByCategoryQuery(bodyPart), ct);
        
        return TypedResults.Ok(routines);
    }
}