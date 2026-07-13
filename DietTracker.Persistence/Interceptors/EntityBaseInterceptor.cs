using System.Security.Claims;
using DietTracker.Persistence.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace DietTracker.Persistence.Interceptors;

public class EntityBaseInterceptor : SaveChangesInterceptor
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    
    public EntityBaseInterceptor(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData,
        InterceptionResult<int> result, CancellationToken ct = default)
    {
        var dbContext = (DietTrackerDbContext?)eventData.Context;

        if (dbContext is null)
        {
            return await base.SavingChangesAsync(eventData, result, ct);
        }

        var entries = dbContext.ChangeTracker.Entries<EntityBase>().ToList();

        if (!entries.Any())
        {
            return await base.SavingChangesAsync(eventData, result, ct);
        }

        foreach (var entry in entries)
        {
            var now = DateTime.UtcNow;
            var entryState = entry.State;
            
            
            if (entryState == EntityState.Added)
            {
                entry.Entity.CreatedAt = now;
            }

            var userId = await GetAuthorizedUserIdentifier();
            if (userId.HasValue && entryState == EntityState.Added)
            {
                entry.Entity.CreatedById = userId.Value;
            }

            if (entryState == EntityState.Modified)
            {
                entry.Entity.UpdatedAt = now;
            }
        }
        
        return await base.SavingChangesAsync(eventData, result, ct);
    }

    private Task<Guid?> GetAuthorizedUserIdentifier()
    {
        var httpContext = _httpContextAccessor.HttpContext;
        
        if (httpContext is null)
        {
            return Task.FromResult<Guid?>(null);
        }

        var userIdClaim = httpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (userIdClaim is null && Guid.TryParse(userIdClaim, out var userIt))
        {
            return Task.FromResult<Guid?>(userIt);
        }
        
        return Task.FromResult<Guid?>(null);
    }
}