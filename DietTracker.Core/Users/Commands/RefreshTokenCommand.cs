using System.Net;
using DietTracker.Core.Auth.Abstractions;
using DietTracker.Core.Common.Abstractions;
using DietTracker.Core.Common.Exceptions;
using DietTracker.Core.Users.Results;
using DispatchR.Abstractions.Send;

namespace DietTracker.Core.Users.Commands;

public class RefreshTokenCommand : IRequest<RefreshTokenCommand, Task<TokenDto>>
{
    public required string RefreshToken { get; init; }  
}

internal class RefreshTokenCommandHandler : TokenCommandHandlerBase, IRequestHandler<RefreshTokenCommand, Task<TokenDto>>
{
    private readonly IRefreshTokenValidationService _refreshTokenValidationService;
    private readonly IAuthorizedUserProvider _authorizedUserProvider;
    public RefreshTokenCommandHandler(ITokenService tokenService, 
        IRefreshTokenValidationService refreshTokenValidationService, 
        IAuthorizedUserProvider authorizedUserProvider) : base(tokenService)
    {
        _refreshTokenValidationService = refreshTokenValidationService;
        _authorizedUserProvider = authorizedUserProvider;
    }

    public async Task<TokenDto> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
    {
        var userId = _authorizedUserProvider.GetCurrentUserId();
        var isTokenValid = await _refreshTokenValidationService.ValidateTokenAsync(userId, request.RefreshToken);

        if (!isTokenValid)
        {
            throw new InvalidTokenException("Invalid refresh token", HttpStatusCode.BadRequest);
        }

        var currentUserClaims = _authorizedUserProvider.GetCurrentUserClaims();

        return await GenerateAndSaveTokensAsync(userId, currentUserClaims);
    }
}