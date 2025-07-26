


using TrainingPlans.API.DTO;
using TrainingPlans.Domain.Models;

namespace TrainingPlans.Persistence.Repositories.Interfaces;

public interface IExercisesRepository
{
    public Task<Guid> Create(ExerciseModel exercise);

    public Task<List<ExerciseModel>> GetAll();
    
    public Task<List<ExerciseModel>> GetAllPrepared();
    
    /*public Task<List<ExerciseModel>> GetAllSelfMade(Guid userId);*/

    public Task<ExerciseModel> Get(Guid id);

    public Task<ExerciseModel> GetByName(string name);

    public Task<List<ExerciseModel>> GetByCategory(string muscleGroup);

    public Task<Dictionary<string, List<CategorizedExercise>>> GetAllCategorized();

    public Task<Guid> Update(Guid id, string name, string muscleGroup);

    public Task<Guid> Delete(Guid id);
}