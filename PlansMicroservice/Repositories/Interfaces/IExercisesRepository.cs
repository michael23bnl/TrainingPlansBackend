using TrainingPlans.Models;

namespace TrainingPlans.Repositories.Interfaces;

public interface IExercisesRepository
{
    public Task<Guid> Create(ExerciseModel exercise);

    public Task<List<ExerciseModel>> GetAll();

    public Task<ExerciseModel> Get(Guid id);

    public Task<Guid> Update(Guid id, string name, string muscleGroup);

    public Task<Guid> Delete(Guid id);
}