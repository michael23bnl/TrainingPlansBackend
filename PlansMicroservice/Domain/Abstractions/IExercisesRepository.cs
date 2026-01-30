using TrainingPlans.Domain.Entities;

namespace TrainingPlans.Domain.Abstractions;

public interface IExercisesRepository
{
    Task<Guid> CreateAsync(string name, string? muscleGroup, CancellationToken ct);
    Task<List<ExerciseEntity>> GetAllAsync(CancellationToken ct);
    Task<ExerciseEntity?> GetAsync(Guid id, CancellationToken ct);
    Task<ExerciseEntity?> GetByNameAsync(string name, CancellationToken ct);
    Task<List<ExerciseEntity>> GetByMuscleGroupAsync(string muscleGroup, CancellationToken ct);
    Task<Dictionary<string, List<ExerciseEntity>>> GetAllByMuscleGroupAsync(CancellationToken ct);
    Task<int> UpdateAsync(Guid id, string name, string muscleGroup, CancellationToken ct);
    Task<int> DeleteAsync(Guid id, CancellationToken ct);
}