using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DietTracker.Persistence.Domain.Workouts;

public class ExerciseItemEntity
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public required int Sets { get; set; }
    public required int Reps { get; set; }
    
    public Guid WorkoutRoutineId { get; set; }
    public WorkoutRoutineEntity WorkoutRoutine { get; set; }
}

internal class ExcerciseItemEntityConfiguration : IEntityTypeConfiguration<ExerciseItemEntity>
{
    public void Configure(EntityTypeBuilder<ExerciseItemEntity> builder)
    {
        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(100);
    }
}