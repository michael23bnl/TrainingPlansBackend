using Microsoft.EntityFrameworkCore;
using TrainingPlans.Domain.Abstractions;
using TrainingPlans.Domain.Entities;

namespace TrainingPlans.Infrastructure.Repositories;

public class ExercisesRepository : IExercisesRepository
{
    private readonly PlansDbContext _context;

    public ExercisesRepository(PlansDbContext context)
    {
        _context = context;
    }

    public async Task<Guid> CreateAsync(string name, string muscleGroup, string description, CancellationToken ct)
    {
        var exercise = new ExerciseEntity
        {
            Id = Guid.NewGuid(),
            Name = name,
            MuscleGroup = muscleGroup,
            Description = description
        };
        
        await _context.Exercises.AddAsync(exercise, ct);
        await _context.SaveChangesAsync(ct);
        
        return exercise.Id;
    }
    
    public async Task<List<ExerciseEntity>> GetAllAsync(CancellationToken ct)
    {
        var exercises = await _context.Exercises
            .AsNoTracking()
            .ToListAsync(ct);

        return exercises;
    }

    public async Task<ExerciseEntity?> GetAsync(Guid id, CancellationToken ct)
    {
        var exercise = await _context.Exercises.FirstOrDefaultAsync(e => e.Id == id, ct);

        return exercise;
    }

    public async Task<ExerciseEntity?> GetByNameAsync(string name, CancellationToken ct)
    {
        var exercise = await _context.Exercises.FirstOrDefaultAsync(e => e.Name == name, ct);
        
        return exercise;
    }

    public async Task<List<ExerciseEntity>> GetByMuscleGroupAsync(string muscleGroup, CancellationToken ct)
    {
        var exercises = await _context.Exercises
            .Where(e => e.MuscleGroup == muscleGroup).ToListAsync(ct);
        
        return exercises;
    }

    public async Task<Dictionary<string, List<ExerciseEntity>>> GetAllByMuscleGroupAsync(CancellationToken ct)
    {
        var exercises = await _context.Exercises
            .AsNoTracking()
            .ToListAsync(ct);

        var categorizedExercises = new Dictionary<string, List<ExerciseEntity>>();

        foreach (var exercise in exercises)
        {
            var categories = exercise.MuscleGroup!
                .Split(',', StringSplitOptions.RemoveEmptyEntries)
                .Select(c => c.Trim())
                .ToList();
            
            foreach (var category in categories)
            {
                if (!categorizedExercises.ContainsKey(category))
                {
                    categorizedExercises[category] = new List<ExerciseEntity>();
                }

                categorizedExercises[category].Add(exercise);
            }
        }

        return categorizedExercises;
    }
    
    public async Task<int> UpdateAsync(Guid id, string name, string muscleGroup, string description, CancellationToken ct)
    {
        var rowsUpdated = await _context.Exercises
            .Where(e => e.Id == id)
            .ExecuteUpdateAsync(s => s
                .SetProperty(e => e.Name, name)
                .SetProperty(e => e.MuscleGroup, muscleGroup)
                .SetProperty(e => e.Description, description), ct);
        
        return rowsUpdated;
    }

    public async Task<int> DeleteAsync(Guid id, CancellationToken ct)
    {
        var rowsDeleted = await _context.Exercises
            .Where(e => e.Id == id)
            .ExecuteDeleteAsync(ct);
        
        return rowsDeleted;
    }
}