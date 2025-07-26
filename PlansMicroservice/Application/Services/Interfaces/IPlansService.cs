using TrainingPlans.API.DTO;
using TrainingPlans.Domain.Models;
using TrainingPlans.Pagination;

namespace TrainingPlans.Application.Services.Interfaces;

public interface IPlansService
{
    public Task<Guid> CreatePlan(PlanRequest request, Guid? userId);
    
    public Task<(int, List<CompletedPlanResponse>)> GetAllSelfMadePlans(PlanParameters planParameters, Guid userId);

    public Task<List<PreparedPlanResponse>> GetAllAvailablePlans(Guid userId);

    public Task<(int, List<PreparedPlanResponse>)> GetAllPreparedPlans(PlanParameters planParameters,
        Guid? userId);

    public Task<PreparedPlanResponse> GetPlan(Guid planId, Guid userId);

    public Task<Guid> UpdatePlan(Guid planId, PlanRequest request, Guid userId);

    public Task<Guid> DeletePlan(Guid id);

    public Task<List<Guid>> GetFavoritePlansIds(Guid userId);

    public Task<List<Guid>> GetCompletedPlansIds(Guid userId);

    public Task<(int totalCount, List<PlanModel> plans)> SearchCatalogPlans(string query, PlanParameters planParameters);

    public Task<(int totalCount, List<PlanModel> plans)> SearchFavoritePlans(string query,
        PlanParameters planParameters, Guid userId);

    public Task<(int totalCount, List<PlanModel> plans)> SearchMyPlans(string query,
        PlanParameters planParameters, Guid userId);

    public Task<(int totalCount, List<PlanModel> plans)> SearchCompletedPlans(string query,
        PlanParameters planParameters, Guid userId);
    
}