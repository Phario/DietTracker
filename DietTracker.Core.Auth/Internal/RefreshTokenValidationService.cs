using DietTracker.Core.Auth.Abstractions;
using DietTracker.Persistence;
using Microsoft.EntityFrameworkCore;

namespace DietTracker.Core.Auth.Internal;

public class RefreshTokenValidationService : IRefreshTokenValidationService
{
    private readonly DietTrackerDbContext _context;
    
    public RefreshTokenValidationService(DietTrackerDbContext context)
    {
        _context = context;
    }

    public async Task<bool> ValidateTokenAsync(Guid userId, string refreshToken)
    {
        var tokenEntity = await _context.RefreshTokens
            .Where(x => x.UserId == userId && x.Token == refreshToken)
            .Select(x => new
            {
                x.Token,
                x.ExpiresAt
            })
            .AsNoTracking()
            .FirstOrDefaultAsync();

        if (tokenEntity == null)
        {
            return false;
        }
        
        return tokenEntity.Token == refreshToken && tokenEntity.ExpiresAt > DateTime.UtcNow;
    }
}