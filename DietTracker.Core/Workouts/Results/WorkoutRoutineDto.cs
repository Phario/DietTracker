using DietTracker.Persistence.Enums;

namespace DietTracker.Core.Workouts;

public class WorkoutRoutineDto
{
    public required Guid Id { get; init; }
    public required string Name { get; init; }
    public string? Description { get; init; }
    public required BodyPart TargetBodyPart { get; init; }
    public required int DurationInMinutes { get; init; }
    public required int CaloriesBurnt { get; init; }
    public ICollection<ExcerciseItemDto> Exercises { get; init; } = [];
}