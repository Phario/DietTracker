namespace DietTracker.Core.Workouts;

public class ExcerciseItemDto
{
    public required string Name { get; init; }
    public required int Sets { get; init; }
    public required int Reps { get; init; }
}