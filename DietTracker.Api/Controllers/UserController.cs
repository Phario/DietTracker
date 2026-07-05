using DietTracker.Core.Users.Commands;
using DietTracker.Core.Users.Results;
using DietTracker.Requests;
using DispatchR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace DietTracker.Controllers;

internal static class UserController
{
    public static IEndpointRouteBuilder MapUsers(this IEndpointRouteBuilder endpoints)
    {
        var group = endpoints.MapGroup("users");
        
        group.WithTags("Users");

        group.MapPost("/login", LoginAsync);
        
        return endpoints;
    }

    private static async Task<Ok<TokenDto>> LoginAsync([FromBody] LoginRequest request, 
        [FromServices] IMediator mediator, CancellationToken ct)
    {
        var result = await mediator.Send(new LoginCommand
        {
            Email = request.Email,
            Password = request.Password
        }, ct);

        return TypedResults.Ok(result);
    }
}