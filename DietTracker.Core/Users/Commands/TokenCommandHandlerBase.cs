using System.Security.Claims;
using DietTracker.Core.Auth.Abstractions;
using DietTracker.Core.Users.Results;

namespace DietTracker.Core.Users.Commands;

internal abstract class TokenCommandHandlerBase
{
    private readonly ITokenService _tokenService;
    
    protected TokenCommandHandlerBase(ITokenService tokenService)
    {
        _tokenService = tokenService;
    }

    protected async Task<TokenDto> GenerateAndSaveTokensAsync(Guid userId, IEnumerable<Claim> claims)
    {
        var accessToken = await _tokenService.GenerateAccessTokenAsync(claims);
        var (refreshToken, expiresAt) = await _tokenService.GenerateRefreshTokenAsync();

        await _tokenService.SaveRefreshTokenAsync(refreshToken, userId, expiresAt);

        return new TokenDto
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken
        };
    }
}