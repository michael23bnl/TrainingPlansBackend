
using TrainingPlans.Application.Abstractions;
using TrainingPlans.Domain.Abstractions;
using TrainingPlans.Domain.Entities;

namespace TrainingPlans.Application.Services;

public class ExercisesService : IExercisesService
{
    private readonly IExercisesRepository _exercisesRepository;

    public ExercisesService(IExercisesRepository exercisesRepository)
    {
        _exercisesRepository = exercisesRepository;
    }

    public async Task<Guid> CreateExerciseAsync(string name, string muscleGroup, string description, CancellationToken ct)
    {
        var exerciseId = await _exercisesRepository.CreateAsync(name, muscleGroup, description, ct);
        
        return exerciseId;
    }

    public async Task<List<ExerciseEntity>> GetAllExercisesAsync(CancellationToken ct)
    {
        var exercises = await _exercisesRepository.GetAllAsync(ct);
        
        return exercises;
    }

    public async Task<ExerciseEntity?> GetExerciseAsync(Guid id, CancellationToken ct)
    {
        var exercise = await _exercisesRepository.GetAsync(id, ct);
        
        return exercise;
    }

    public async Task<ExerciseEntity?> GetExerciseByNameAsync(string name, CancellationToken ct)
    {
        var exercise = await _exercisesRepository.GetByNameAsync(name, ct);

        return exercise;
    }

    public async Task<List<ExerciseEntity>> GetExercisesByMuscleGroupAsync(string muscleGroup, CancellationToken ct)
    {
        var exercises = await _exercisesRepository.GetByMuscleGroupAsync(muscleGroup, ct);

        return exercises;
    }

    public async Task<Dictionary<string, List<ExerciseEntity>>> GetAllExercisesByMuscleGroupAsync(CancellationToken ct)
    {
        var exercises = await _exercisesRepository.GetAllByMuscleGroupAsync(ct);

        return exercises;
    }

    public async Task<Guid> UpdateExerciseAsync(Guid id, string name, string muscleGroup, 
        string description, CancellationToken ct)
    {
        var rowsUpdated = await _exercisesRepository
            .UpdateAsync(id, name, muscleGroup,  description, ct);
        
        return id;
    }

    public async Task<Guid> DeleteExerciseAsync(Guid id, CancellationToken ct)
    {
        var rowsDeleted = await _exercisesRepository.DeleteAsync(id, ct);
        
        return id;
    }
}