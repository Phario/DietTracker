using System.Security.Claims;
using DietTracker.Core.Auth.Abstractions;

namespace DietTracker.Core.Auth.Internal;

public class TokenService : ITokenService
{
    //TODO: implement TokenService (test new token)
    public async Task<string> GenerateAccessTokenAsync(IEnumerable<Claim> claims)
    {
        throw new NotImplementedException();
    }

    public async Task<(string, DateTime)> GenerateRefreshTokenAsync()
    {
        throw new NotImplementedException();
    }

    public async Task SaveRefreshTokenAsync(string refreshToken, Guid userId, DateTime expiresAt)
    {
        throw new NotImplementedException();
    }
}