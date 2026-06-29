using DietTracker.Persistence;
using DietTracker.Persistence.Domain.Meals;
using DietTracker.Persistence.Enums;
using DispatchR.Abstractions.Send;

namespace DietTracker.Core.Meals.Commands;

public record CreateMealCommand : IRequest<CreateMealCommand, Task<Guid>>
{
    public required string Name { get; init; }
    public required double Calories { get; init; }
    public required double Protein { get; init; }
    public required double Carbs { get; init; }
    public required double Fats { get; init; }
    public HashSet<MealTags>? Tags { get; init; } = [];
}

internal class CreateMealCommandHandler : IRequestHandler<CreateMealCommand, Task<Guid>>
{
    private readonly DietTrackerDbContext _context;

    public CreateMealCommandHandler(DietTrackerDbContext context)
    {
        _context = context;
    }
    
    public async Task<Guid> Handle(CreateMealCommand request, CancellationToken ct)
    {
        var meal = new MealEntity
        {
            Name = request.Name,
            Calories = request.Calories,
            Carbs = request.Carbs,
            Protein = request.Protein,
            Fats = request.Fats,
            Tags = request.Tags.Distinct().ToList()
        };
        
        _context.Meals.Add(meal);
        await _context.SaveChangesAsync(ct);
        
        return meal.Id;
    }
}