using TrainingPlans.Models;

namespace TrainingPlans.Repositories.Interfaces;

public interface IPlansRepository
{
    public Task<Guid> Create(PlanModel plan);

    public Task<List<PlanModel>> GetAll();

    public Task<PlanModel> Get(Guid id);

    public Task<Guid> Update(Guid id, List<ExerciseModel> exercises);


    public Task<Guid> Delete(Guid id);
}