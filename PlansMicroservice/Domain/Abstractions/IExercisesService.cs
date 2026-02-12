using TrainingPlans.Domain.Entities;

namespace TrainingPlans.Domain.Abstractions;

public interface IExercisesService
{
    Task<Guid> CreateExerciseAsync(string name, string muscleGroup, string description, CancellationToken ct);
    Task<List<ExerciseEntity>> GetAllExercisesAsync(CancellationToken ct);
    Task<ExerciseEntity?> GetExerciseAsync(Guid id, CancellationToken ct);
    Task<ExerciseEntity?> GetExerciseByNameAsync(string name, CancellationToken ct);
    Task<List<ExerciseEntity>> GetExercisesByMuscleGroupAsync(string muscleGroup, CancellationToken ct);
    Task<Dictionary<string, List<ExerciseEntity>>> GetAllExercisesByMuscleGroupAsync(CancellationToken ct);
    Task<Guid> UpdateExerciseAsync(Guid id, string name, string muscleGroup, string description, CancellationToken ct);
    Task<Guid> DeleteExerciseAsync(Guid id, CancellationToken ct);
}