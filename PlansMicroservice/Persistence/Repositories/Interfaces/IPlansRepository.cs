
using TrainingPlans.API.DTO;
using TrainingPlans.Domain.Models;
using TrainingPlans.Pagination;

namespace TrainingPlans.Persistence.Repositories.Interfaces;

public interface IPlansRepository
{
    public Task<PreparedPlanResponse> Get(Guid planId, Guid? userId);
    
    public Task<Guid> Create(PlanModel plan);
    
    public Task<Guid> Update(Guid id, string? name, List<ExerciseModel> exercises);
    
    public Task<Guid> Delete(Guid id);
    
    public Task<List<PlanModel>> GetAllPrepared();
    
    public Task<(int, List<PreparedPlanResponse>)> GetAllPrepared(Guid? userId, PlanParameters planParameters);
    
    public Task<(int, List<CompletedPlanResponse>)> GetAllSelfMade(Guid userId, PlanParameters planParameters);
    
    public Task<List<PreparedPlanResponse>> GetAllAvailable(Guid userId);
    
    public Task<List<Guid>> GetFavoritePlanIds(Guid userId);
    
    public Task<List<Guid>> GetCompletedPlanIds(Guid userId);

    public Task<(int totalCount, List<PlanModel> plans)> Search(string query, PlanParameters planParameters,
        Guid? filter);

    public Task<(int totalCount, List<PlanModel> plans)> Search(string query, PlanParameters planParameters,
        List<Guid> filter);




}