using DietTracker.Core.Users.Results;
using DispatchR.Abstractions.Send;

namespace DietTracker.Core.Users.Commands;

public record LoginCommand : IRequest<LoginCommand, Task<TokenDto>>
{
    public required string Email { get; init; }
    public required string Password { get; init; }  
}