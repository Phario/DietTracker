using DietTracker.Persistence;
using DietTracker.Persistence.Enums;
using DispatchR.Abstractions.Send;
using Microsoft.EntityFrameworkCore;

namespace DietTracker.Core.Workouts.Queries;

public record GetWorkoutsByCategoryQuery(BodyPart TargetBodyPart) : IRequest<GetWorkoutsByCategoryQuery, Task<IEnumerable<WorkoutRoutineDto>>>;

internal class GetWorkoutsByCategoryQueryHandler : IRequestHandler<GetWorkoutsByCategoryQuery, Task<IEnumerable<WorkoutRoutineDto>>>
{
    private readonly DietTrackerDbContext _context;

    public GetWorkoutsByCategoryQueryHandler(DietTrackerDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<WorkoutRoutineDto>> Handle(GetWorkoutsByCategoryQuery request, CancellationToken ct)
    {
        return await _context.Workouts
            .Where(x => x.TargetBodyPart == request.TargetBodyPart)
            .Select(x => new WorkoutRoutineDto
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                TargetBodyPart =  x.TargetBodyPart,
                DurationInMinutes =  x.DurationInMinutes,
                CaloriesBurnt = x.CaloriesBurnt,
                Exercises = x.Exercises.Select(e => new ExcerciseItemDto
                {
                    Name = e.Name,
                    Sets = e.Sets,
                    Reps = e.Reps
                }).ToList()
            })
            .AsNoTracking()
            .ToListAsync(ct);
    }
}