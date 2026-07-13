using System.Security.Claims;
using DietTracker.Core.Common.Abstractions;
using Microsoft.AspNetCore.Http;

namespace DietTracker.Core.Common.Internal;

internal sealed class AuthorizedUserProvider : IAuthorizedUserProvider
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    
    public AuthorizedUserProvider(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }
    
    public Guid GetCurrentUserId()
    {
        var currentUserId = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        
        return currentUserId is not null ? Guid.Parse(currentUserId) : throw new UnauthorizedAccessException();
    }

    public IEnumerable<Claim>? GetCurrentUserClaims()
    {
        return _httpContextAccessor.HttpContext?.User.Claims;
    }
}