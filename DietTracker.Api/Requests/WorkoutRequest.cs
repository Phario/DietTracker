using DietTracker.Persistence.Enums;

namespace DietTracker.Requests;

public record ExerciseItemRequest
{
    public required string Name { get; init; }
    public required int Sets { get; init; }
    public required int Reps { get; init; }
}

public record CreateWorkoutRoutineRequest
{
    public required string Name { get; init; }
    public string? Description { get; init; }
    public required BodyPart TargetBodyPart { get; init; }
    public required int DurationInMinutes { get; init; }
    public required int CaloriesBurnt { get; init; }
    public ICollection<ExerciseItemRequest> Exercises { get; init; } = [];
}