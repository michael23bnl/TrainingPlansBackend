
using TrainingPlans.API.DTO;
using TrainingPlans.Application.Services.Interfaces;
using TrainingPlans.Domain.Models;
using TrainingPlans.Persistence.Repositories.Interfaces;

namespace TrainingPlans.Application.Services;

public class ExercisesService : IExercisesService
{
    private readonly IExercisesRepository _exercisesRepository;

    public ExercisesService(IExercisesRepository exercisesRepository)
    {
        _exercisesRepository = exercisesRepository;
    }

    public async Task<Guid> CreateExercise(ExerciseRequest request)
    {

        var (exercise, response) = ExerciseModel
            .Create(Guid.NewGuid(), request.Name, request.MuscleGroup);

        if (response != "Exercise has been created")
        {
            return Guid.Empty;
        }

        var exerciseId = await _exercisesRepository.Create(exercise);
        return exerciseId;
    }

    public async Task<List<ExerciseModel>> GetAllExercises()
    {
        var exercises = await _exercisesRepository.GetAllPrepared();
        return exercises;
    }

    public async Task<ExerciseModel> GetExercise(Guid exerciseId)
    {
        var exercise = await _exercisesRepository.Get(exerciseId);
        return exercise;
    }

    public async Task<ExerciseModel> GetExerciseByName(string name)
    {
        var exercise = await _exercisesRepository.GetByName(name);

        return exercise;
    }

    public async Task<List<ExerciseModel>> GetExercisesByCategory(string muscleGroup)
    {
        var exercises = await _exercisesRepository.GetByCategory(muscleGroup);

        return exercises;
    }

    public async Task<Dictionary<string, List<CategorizedExercise>>> GetAllExercisesCategorized()
    {
        var exercises = await _exercisesRepository.GetAllCategorized();

        return exercises;
    }

    public async Task<Guid> UpdateExercise(Guid exerciseId, ExerciseRequest request)
    {
        var updatedExerciseId = await _exercisesRepository
            .Update(exerciseId, request.Name, request.MuscleGroup);
        
        return updatedExerciseId;
    }

    public async Task<Guid> DeleteExercise(Guid exerciseId)
    {
        var deletedExerciseId = await _exercisesRepository.Delete(exerciseId);
        
        return deletedExerciseId;
    }
}