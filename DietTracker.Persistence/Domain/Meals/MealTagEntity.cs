using DietTracker.Persistence.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DietTracker.Persistence.Domain.Meals;

public class MealTagEntity
{
    public Guid MealId { get; set; }
    public MealTag Tag { get; set; }
    public MealEntity Meal { get; set; }
}

internal class MealTagEntityConfiguration : IEntityTypeConfiguration<MealTagEntity>
{
    public void Configure(EntityTypeBuilder<MealTagEntity> builder)
    {
        builder.HasKey(x => new { x.MealId, x.Tag });
        
        builder
            .Property(x => x.Tag)
            .HasConversion<string>()
            .HasMaxLength(30);
        
        builder.HasIndex(x => x.Tag);  
    }
}