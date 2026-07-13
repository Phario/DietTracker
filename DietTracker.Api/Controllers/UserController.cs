using DietTracker.Core.Users.Commands;
using DietTracker.Core.Users.Results;
using DietTracker.Persistence.Enums;
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
        group.MapPost("/register", RegisterAsync);
        
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

    private static async Task<Ok<TokenDto>> RegisterAsync([FromBody] RegisterRequest request,
        [FromServices] IMediator mediator, CancellationToken ct)
    {
        var result = await mediator.Send(new RegisterCommand
        {
            Email = request.Email,
            Password = request.Password,
            Name = request.Name,
            UserRole = RoleNames.User
        }, ct);
        
        return TypedResults.Ok(result);
    }
}