
using TrainingPlans.Domain.Models;
using TrainingPlans.Pagination;

namespace TrainingPlans.Application.Services.Interfaces;

public interface IPlansSearch
{
    Task CreateIndexIfNotExistsAsync(string indexName);

    Task<bool> ContainsDocuments(string indexName);

    Task<bool> AddOrUpdateAsync(PlanModel plan);
    
    Task<bool> AddOrUpdateBulk(IEnumerable<PlanModel> plans);
    
    Task<PlanModel> GetAsync(string id);
    
    Task<List<PlanModel>?> GetAllAsync();
    
    Task<bool> RemoveAsync(string id);

    Task<long?> RemoveAll();

    Task<(int totalCount, List<PlanModel> plans)> SearchPlansAsync(string query, PlanParameters planParameters);

    Task<(int totalCount, List<PlanModel> plans)> SearchThroughMyPlans(string query, Guid userId, PlanParameters planParameters);

    Task<(int totalCount, List<PlanModel> plans)> SearchThroughFavoritePlans(string query, List<Guid> favoritePlanIds, PlanParameters planParameters);

    Task<(int totalCount, List<PlanModel> plans)> SearchThroughCompletedPlans(string query, List<Guid> completedPlanIds, PlanParameters planParameters);
}