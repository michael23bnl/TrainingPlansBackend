using TrainingPlans.Entities;
using TrainingPlans.Pagination;

namespace TrainingPlans.Services;

public interface IElasticService
{
    Task CreateIndexIfNotExistsAsync(string indexName);

    Task<bool> ContainsDocuments(string indexName);

    Task<bool> AddOrUpdateAsync(PlanEntity plan);
    
    Task<bool> AddOrUpdateBulk(IEnumerable<PlanEntity> plans);
    
    Task<PlanEntity> GetAsync(string id);
    
    Task<List<PlanEntity>?> GetAllAsync();
    
    Task<bool> RemoveAsync(string id);

    Task<long?> RemoveAll();

    Task<(int totalCount, List<PlanEntity> plans)> SearchPlansAsync(string query, PlanParameters planParameters);

    Task<(int totalCount, List<PlanEntity> plans)> SearchThroughMyPlans(string query, Guid userId, PlanParameters planParameters);

    Task<(int totalCount, List<PlanEntity> plans)> SearchThroughFavoritePlans(string query, List<Guid> favoritePlanIds, PlanParameters planParameters);

    Task<(int totalCount, List<PlanEntity> plans)> SearchThroughCompletedPlans(string query, List<Guid> completedPlanIds, PlanParameters planParameters);

}