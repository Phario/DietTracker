using System.Security.Claims;

namespace DietTracker.Core.Common.Abstractions;

public interface IAuthorizedUserProvider
{
    Guid GetCurrentUserId();
    IEnumerable<Claim>? GetCurrentUserClaims();
}