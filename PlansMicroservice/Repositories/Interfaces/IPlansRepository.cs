
using TrainingPlans.Contracts;
using TrainingPlans.Models;
using TrainingPlans.Pagination;

namespace TrainingPlans.Repositories.Interfaces;

public interface IPlansRepository
{
    public Task<Guid> Create(PlanModel plan);

    public Task<List<PlanModel>> GetAll();
    
    public Task<List<PlanModel>> GetAllPrepared();
    
    public Task<(int, List<PreparedPlanResponse>)> GetAllPrepared(Guid? userId, PlanParameters planParameters);
    
    public Task<(int, List<CompletedPlanResponse>)> GetAllSelfMade(Guid userId, PlanParameters planParameters);
    
    public Task<List<PreparedPlanResponse>> GetAllAvailable(Guid userId);
    
    public Task<PreparedPlanResponse> Get(Guid planId, Guid? userId);

    public Task<PlanModel> GetByName(Guid userId, string name);
    
    public Task<PlanModel> GetPreparedByName(string name);


    public Task<List<Guid>> GetFavoritePlanIds(Guid userId);
    
    public Task<List<Guid>> GetCompletedPlanIds(Guid userId);

    public Task<Guid> Update(Guid id, string? name, List<ExerciseModel> exercises);


    public Task<Guid> Delete(Guid id);
}