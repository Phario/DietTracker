using DietTracker.Persistence.Domain;
using DietTracker.Persistence.Domain.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DietTracker.Persistence;

public static class ServiceExtensions
{
    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        services.AddDbContext<DietTrackerDbContext>(options =>
            options.UseNpgsql(connectionString));

        services.AddIdentity<UserEntity, RoleEntity>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequiredLength = 8;
                options.SignIn.RequireConfirmedEmail = false;
                options.SignIn.RequireConfirmedPhoneNumber = false;
            })
            .AddRoles<RoleEntity>()
            .AddEntityFrameworkStores<DietTrackerDbContext>()
            .AddDefaultTokenProviders();
        
        return services;
    }
}