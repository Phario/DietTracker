using DietTracker.Core.Meals.Results;
using DietTracker.Persistence;
using DispatchR.Abstractions.Send;
using Microsoft.EntityFrameworkCore;

namespace DietTracker.Core.Meals.Queries;

public record GetAllMealsQuery : IRequest<GetAllMealsQuery, Task<IEnumerable<MealDto>>>;

internal class GetAllMealsQueryHandler : IRequestHandler<GetAllMealsQuery, Task<IEnumerable<MealDto>>>
{
    private readonly DietTrackerDbContext _context;
    
    public GetAllMealsQueryHandler(DietTrackerDbContext context)
    {
        _context = context;
    }
    
    public async Task<IEnumerable<MealDto>> Handle(GetAllMealsQuery request, CancellationToken ct)
    {
        return await _context.Meals
            .Select(x => new MealDto
            {
                Name = x.Name,
                Calories = x.Calories,
                Protein = x.Protein,
                Carbs = x.Carbs,
                Fats = x.Fats,
                Tags = x.Tags
            })
            .AsNoTracking()
            .ToListAsync(ct);
    }
}