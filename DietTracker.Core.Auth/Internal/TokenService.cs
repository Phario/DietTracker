using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using DietTracker.Core.Auth.Abstractions;
using DietTracker.Core.Auth.Settings;
using DietTracker.Persistence;
using DietTracker.Persistence.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace DietTracker.Core.Auth.Internal;

public class TokenService : ITokenService
{
    private readonly JwtSettings _jwtSettings;
    private readonly DietTrackerDbContext _dbContext;
    
    public TokenService(IOptions<JwtSettings> jwtSettings, DietTrackerDbContext dbContext)
    {
        _dbContext = dbContext;
        _jwtSettings = jwtSettings.Value;
    }
    
    public Task<string> GenerateAccessTokenAsync(IEnumerable<Claim> claims)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var expires = DateTime.UtcNow.AddMinutes(_jwtSettings.ExpiresInMinutes );

        var token = new JwtSecurityToken(
            issuer: _jwtSettings.Issuer,
            audience: _jwtSettings.Audience,
            claims: claims,
            expires: expires,
            signingCredentials: creds);

        return Task.FromResult(new JwtSecurityTokenHandler().WriteToken(token));
    }

    public  Task<(string, DateTime)> GenerateRefreshTokenAsync()
    {
        var randomNumber = new byte[32];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomNumber);
        
        return Task.FromResult((Convert.ToBase64String(randomNumber), DateTime.UtcNow.AddDays(_jwtSettings.RefreshTokenExpiresInDays)));
    }

    public async Task SaveRefreshTokenAsync(string refreshToken, Guid userId, DateTime expiresAt)
    {
        var existingToken = await _dbContext.RefreshTokens
            .FirstOrDefaultAsync(x => x.UserId == userId);

        if (existingToken is not null)
        {
            _dbContext.RefreshTokens.Remove(existingToken);
        }
        
        var refreshTokenEntity = new RefreshTokenEntity
        {
            Token = refreshToken,
            ExpiresAt = expiresAt,
            UserId = userId
        };
        
        _dbContext.Add(refreshTokenEntity);
        await _dbContext.SaveChangesAsync();
    }
}