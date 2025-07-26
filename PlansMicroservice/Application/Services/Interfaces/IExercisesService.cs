using TrainingPlans.API.DTO;
using TrainingPlans.Domain.Models;

namespace TrainingPlans.Application.Services.Interfaces;

public interface IExercisesService
{
    Task<Guid> CreateExercise(ExerciseRequest request);
    Task<List<ExerciseModel>> GetAllExercises();
    Task<ExerciseModel> GetExercise(Guid exerciseId);
    Task<ExerciseModel> GetExerciseByName(string name);
    Task<List<ExerciseModel>> GetExercisesByCategory(string muscleGroup);
    Task<Dictionary<string, List<CategorizedExercise>>> GetAllExercisesCategorized();
    Task<Guid> UpdateExercise(Guid exerciseId, ExerciseRequest request);
    Task<Guid> DeleteExercise(Guid exerciseId);
}