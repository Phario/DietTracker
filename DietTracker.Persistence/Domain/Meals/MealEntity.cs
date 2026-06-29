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
    public ICollection<MealTags>? Tags { get; set; }
}