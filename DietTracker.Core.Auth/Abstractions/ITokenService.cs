using System.Security.Claims;

namespace DietTracker.Core.Auth.Abstractions;

public interface ITokenService
{
    Task<string> GenerateAccessTokenAsync(IEnumerable<Claim> claims);
    Task<(string, DateTime)> GenerateRefreshTokenAsync();
    Task SaveRefreshTokenAsync(string refreshToken, Guid userId, DateTime expiresAt);
}