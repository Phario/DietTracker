using Bogus;
using DietTracker.Persistence.Consts;
using DietTracker.Persistence.Domain;
using DietTracker.Persistence.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DietTracker.Persistence.Seeding;

public class UserSeeder
{
    public static async ValueTask SeedUsersAsync(DietTrackerDbContext context, UserManager<UserEntity> userManager)
    {
        var existingAdmin = await userManager.FindByEmailAsync(AdminConsts.AdminEmail);

        var existingUserCount = await context.Users.AsNoTracking().CountAsync();

        if (existingAdmin == null)
        {
            var adminUser = new UserEntity
            {
                UserName = AdminConsts.AdminUsername,
                Email = AdminConsts.AdminEmail,
                EmailConfirmed = true
            };
            
            var result = await userManager.CreateAsync(adminUser, AdminConsts.AdminPassword);

            if (result.Succeeded)
            {
                var userFromDb = await userManager.FindByEmailAsync(adminUser.Email);
                if (userFromDb != null)
                {
                    await userManager.AddToRoleAsync(userFromDb, nameof(RoleNames.Admin));
                }
            }
        }

        if (existingUserCount == 1)
        {
            var faker = new Faker<UserEntity>("pl");
            
            faker.RuleFor(x => x.UserName, f => f.Internet.UserName());
            faker.RuleFor(x => x.Email, f => f.Internet.Email());
            faker.RuleFor(x => x.EmailConfirmed, true);
            
            var users = faker.Generate(50);
            context.Users.AddRange(users);
            await context.SaveChangesAsync();
        }
    }
}