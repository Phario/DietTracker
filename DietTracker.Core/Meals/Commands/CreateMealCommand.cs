using DietTracker.Core.Common.Abstractions;
using DietTracker.Persistence;
using DietTracker.Persistence.Domain.Meals;
using DietTracker.Persistence.Enums;
using DispatchR.Abstractions.Send;
using Microsoft.EntityFrameworkCore;

namespace DietTracker.Core.Meals.Commands;

public record CreateMealCommand : IRequest<CreateMealCommand, Task<Guid>>
{
    public required string Name { get; init; }
    public required double Calories { get; init; }
    public required double Protein { get; init; }
    public required double Carbs { get; init; }
    public required double Fats { get; init; }
    public HashSet<MealTag>? Tags { get; init; } = [];
}

internal class CreateMealCommandHandler : IRequestHandler<CreateMealCommand, Task<Guid>>
{
    private readonly DietTrackerDbContext _context;
    private readonly IAuthorizedUserProvider _authorizedUserProvider;

    public CreateMealCommandHandler(DietTrackerDbContext context, IAuthorizedUserProvider authorizedUserProvider)
    {
        _context = context;
        _authorizedUserProvider = authorizedUserProvider;
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
        };
        
        _context.Meals.Add(meal);
        
        meal.Tags = request.Tags?
            .Select(x => new MealTagEntity { MealId = meal.Id, Tag = x }).ToList() ?? [];
        await _context.SaveChangesAsync(ct);
        
        return meal.Id;
    }
}