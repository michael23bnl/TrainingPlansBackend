using TrainingPlans.Contracts;
using TrainingPlans.Models;

namespace TrainingPlans.Repositories.Interfaces;

public interface IPlansRepository
{
    public Task<Guid> Create(PlanModel plan);

    public Task<List<PlanModel>> GetAll();
    
    public Task<List<PlanModel>> GetAllPrepared();
    
    public Task<List<PreparedPlanResponse>> GetAllPrepared(Guid? userId);
    
    public Task<List<PlanModel>> GetAllSelfMade(Guid userId);

    public Task<PlanModel> Get(Guid id);

    public Task<Guid> Update(Guid id, string? name, List<ExerciseModel> exercises);


    public Task<Guid> Delete(Guid id);
}