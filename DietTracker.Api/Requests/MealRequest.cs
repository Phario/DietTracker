using DietTracker.Persistence.Enums;

namespace DietTracker.Requests;

public record CreateMealRequest
{
    public required string Name { get; init; }
    public required double Calories { get; init; }
    public required double Protein { get; init; }
    public required double Carbs { get; init; }
    public required double Fats { get; init; }
    public ICollection<MealTags>? Tags { get; init; } = [];
}