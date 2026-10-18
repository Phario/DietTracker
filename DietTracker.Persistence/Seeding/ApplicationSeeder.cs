using DietTracker.Persistence.Domain;
using DietTracker.Persistence.Domain.Users;
using Microsoft.AspNetCore.Identity;

namespace DietTracker.Persistence.Seeding;

public class ApplicationSeeder
{
    public static async Task SeedAsync(DietTrackerDbContext context, UserManager<UserEntity> userManager, 
        RoleManager<RoleEntity> roleManager)
    {
        await RoleSeeder.SeedRolesAsync(context, roleManager);
        await UserSeeder.SeedUsersAsync(context, userManager);
    }
}