using DietTracker.Persistence.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DietTracker.Persistence.Domain.Workouts;

public class WorkoutRoutineEntity : EntityBase
{
    public required string Name { get; set; }
    public string? Description { get; set; }
    public required BodyPart TargetBodyPart { get; set; }
    public required int DurationInMinutes { get; set; }
    public required int CaloriesBurnt { get; set; }

    public ICollection<ExerciseItemEntity>? Exercises { get; set; }
}

internal class WorkoutRoutineEntityConfiguration : IEntityTypeConfiguration<WorkoutRoutineEntity>
{
    public void Configure(EntityTypeBuilder<WorkoutRoutineEntity> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.TargetBodyPart)
            .HasConversion<string>()
            .HasMaxLength(40);

        builder.HasIndex(x => x.TargetBodyPart);

        builder
            .HasOne(x => x.CreatedBy)
            .WithMany()
            .HasForeignKey(x => x.CreatedById);

        builder
            .HasMany(x => x.Exercises)
            .WithOne(x => x.WorkoutRoutine)
            .HasForeignKey(x => x.WorkoutRoutineId);
    }
}