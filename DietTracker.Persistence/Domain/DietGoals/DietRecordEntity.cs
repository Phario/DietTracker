using DietTracker.Persistence.Domain.Meals;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DietTracker.Persistence.Domain.DietGoals;

public class DietRecordEntity : EntityBase
{
    public required DateTime Date { get; set; }
    public MealEntity Meal { get; set; }
}

internal class DietRecordEntityConfiguration : IEntityTypeConfiguration<DietRecordEntity>
{
    public void Configure(EntityTypeBuilder<DietRecordEntity> builder)
    {
        builder.HasKey(x => x.Id);
        
        builder.HasOne(x => x.CreatedBy)
            .WithMany(x => x.DietRecords)
            .HasForeignKey(x => x.CreatedById);
    }
}