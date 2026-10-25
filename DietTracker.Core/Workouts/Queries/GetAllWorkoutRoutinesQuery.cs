using DietTracker.Persistence;
using DispatchR.Abstractions.Send;
using Microsoft.EntityFrameworkCore;

namespace DietTracker.Core.Workouts.Queries;

public record GetAllWorkoutRoutinesQuery : IRequest<GetAllWorkoutRoutinesQuery, Task<IEnumerable<WorkoutRoutineDto>>>;

internal class GetAllWorkoutRoutinesQueryHandler : IRequestHandler<GetAllWorkoutRoutinesQuery, Task<IEnumerable<WorkoutRoutineDto>>>
{
    private readonly DietTrackerDbContext _context;

    public GetAllWorkoutRoutinesQueryHandler(DietTrackerDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<WorkoutRoutineDto>> Handle(GetAllWorkoutRoutinesQuery request, CancellationToken ct)
    {
        return await _context.Workouts
            .Select(x => new WorkoutRoutineDto
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                TargetBodyPart =  x.TargetBodyPart,
                DurationInMinutes =  x.DurationInMinutes,
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