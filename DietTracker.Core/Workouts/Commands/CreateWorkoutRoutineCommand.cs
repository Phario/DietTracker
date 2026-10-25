using DietTracker.Core.Common.Abstractions;
using DietTracker.Persistence;
using DietTracker.Persistence.Domain.Workouts;
using DietTracker.Persistence.Enums;
using DispatchR.Abstractions.Send;

namespace DietTracker.Core.Workouts.Commands;

public record ExerciseItemInput
{
    public required string Name { get; init; }
    public required int Sets { get; init; }
    public required int Reps { get; init; }
}

public record CreateWorkoutRoutineCommand : IRequest<CreateWorkoutRoutineCommand, Task<Guid>>
{
    public required string Name { get; init; }
    public string? Description { get; init; }
    public required BodyPart TargetBodyPart { get; init; }
    public required int DurationInMinutes { get; init; }
    public ICollection<ExerciseItemInput> Exercises { get; init; } = [];
}

internal class CreateWorkoutRoutineCommandHandler : IRequestHandler<CreateWorkoutRoutineCommand, Task<Guid>>
{
    private readonly DietTrackerDbContext _context;
    private readonly IAuthorizedUserProvider _authorizedUserProvider;
    
    public CreateWorkoutRoutineCommandHandler(DietTrackerDbContext context, IAuthorizedUserProvider authorizedUserProvider)
    {
        _context = context;
        _authorizedUserProvider = authorizedUserProvider;
    }
    
    public async Task<Guid> Handle(CreateWorkoutRoutineCommand request, CancellationToken ct)
    {
        var routine = new WorkoutRoutineEntity
        {
            Name = request.Name,
            Description = request.Description,
            TargetBodyPart = request.TargetBodyPart,
            DurationInMinutes = request.DurationInMinutes,
            Exercises = request.Exercises.Select(x => new ExerciseItemEntity
            {
                Name = x.Name,
                Sets = x.Sets,
                Reps = x.Reps
            }).ToList()
        };
        
        _context.Workouts.Add(routine);
        await _context.SaveChangesAsync(ct);
        
        return routine.Id;
    }
}