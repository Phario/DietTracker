using DietTracker.Persistence.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DietTracker.Persistence.Domain.Meals;

public class MealEntity : EntityBase
{
    public required string Name { get; set; }
    public string? Description { get; set; }
    public required double Calories { get; set; }
    public required double Protein { get; set; }
    public required double Carbs { get; set; }
    public required double Fats { get; set; }
    public ICollection<MealTagEntity>? Tags { get; set; }
    public bool IsIngredient { get; set; }
}

internal class MealEntityConfiguration : IEntityTypeConfiguration<MealEntity>
{
    public void Configure(EntityTypeBuilder<MealEntity> builder)
    {
        builder.HasKey(x => x.Id);

        builder
            .HasOne(x => x.CreatedBy)
            .WithMany(x => x.Meals)
            .HasForeignKey(x => x.CreatedById);
    }
}