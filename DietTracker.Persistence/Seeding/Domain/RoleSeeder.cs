using DietTracker.Persistence.Domain.Users;
using DietTracker.Persistence.Enums;
using Microsoft.AspNetCore.Identity;

namespace DietTracker.Persistence.Seeding;

public class RoleSeeder
{
    public static async ValueTask SeedRolesAsync(DietTrackerDbContext context, RoleManager<RoleEntity> roleManager)
    {
        foreach (var roleName in Enum.GetNames(typeof(RoleNames)))
        {
            if (!await roleManager.RoleExistsAsync(roleName))
            {
                await roleManager.CreateAsync(new RoleEntity { Name = roleName });
            }
        }
    }
}