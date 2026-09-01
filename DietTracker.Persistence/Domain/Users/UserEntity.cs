using DietTracker.Persistence.Domain.DietGoals;
using DietTracker.Persistence.Domain.Meals;
using DietTracker.Persistence.Domain.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DietTracker.Persistence.Domain;

public class UserEntity : IdentityUser<Guid>
{
    public string Name { get; set; }
    public RefreshTokenEntity? RefreshToken { get; set; }
    public ICollection<MealEntity> Meals { get; set; } = [];
    public ICollection<DietRecordEntity> DietRecords { get; set; } = [];
}

internal class UserEntityConfiguration : IEntityTypeConfiguration<UserEntity>
{
    public void Configure(EntityTypeBuilder<UserEntity> builder)
    {
        builder.HasMany(x => x.Meals);
        builder.HasMany(x => x.DietRecords);
    }
}