using DietTracker.Persistence.Domain.Meals;

namespace DietTracker.Persistence.Domain.DietGoals;

public class DietGoalEntity : EntityBase
{
    public required DateOnly Date { get; set; }
    public ICollection<MealEntity> Meals { get; set; }
}