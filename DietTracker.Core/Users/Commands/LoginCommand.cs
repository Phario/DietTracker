using System.Security.Claims;
using DietTracker.Core.Auth.Abstractions;
using DietTracker.Core.Users.Results;
using DietTracker.Persistence.Domain;
using DispatchR.Abstractions.Send;
using Microsoft.AspNetCore.Identity;

namespace DietTracker.Core.Users.Commands;

public record LoginCommand : IRequest<LoginCommand, Task<TokenDto>>
{
    public required string Email { get; init; }
    public required string Password { get; init; }  
}

internal sealed class LoginCommandHandler : TokenCommandHandlerBase, IRequestHandler<LoginCommand, Task<TokenDto>>
{
    private readonly SignInManager<UserEntity> _signInManager;
    private readonly UserManager<UserEntity> _userManager;

    public LoginCommandHandler(UserManager<UserEntity> userManager, SignInManager<UserEntity> signInManager, 
        ITokenService tokenService) : base(tokenService)
    {
        _signInManager = signInManager;
        _userManager = userManager;
    }

    public async Task<TokenDto> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);

        if (user is null)
        {
            throw new Exception(); // TODO: throw custom exception
        }

        var isPasswordValid = await _userManager.CheckPasswordAsync(user, request.Password);

        if (!isPasswordValid)
        {
            throw new UnauthorizedAccessException("Invalid login credentials");
        }
        
        var currentRoles = await _userManager.GetRolesAsync(user);

        IList<Claim> claims =
        [
            new (ClaimTypes.NameIdentifier, user.Id.ToString()),
            new (ClaimTypes.Email, request.Email),
            ..currentRoles.Select(r => new Claim(ClaimTypes.Role, r))
        ];
        
        return await GenerateAndSaveTokensAsync(user.Id, claims);
    }
}