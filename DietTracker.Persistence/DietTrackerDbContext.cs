using DietTracker.Persistence.Domain;
using DietTracker.Persistence.Domain.Meals;
using DietTracker.Persistence.Domain.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DietTracker.Persistence;

public class DietTrackerDbContext : IdentityDbContext<UserEntity , RoleEntity, Guid>
{
    public DietTrackerDbContext()
    {
    }

    public virtual DbSet<MealEntity> Meals => Set<MealEntity>();
    
    public DietTrackerDbContext(DbContextOptions<DietTrackerDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(typeof(DietTrackerDbContext).Assembly);
        builder.Ignore(typeof(IdentityPasskeyData));
        
        base.OnModelCreating(builder);
    }
}