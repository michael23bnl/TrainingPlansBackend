using TrainingPlans.Entities;

namespace TrainingPlans.Services;

public interface IElasticService
{
    Task CreateIndexIfNotExistsAsync(string indexName);

    Task<bool> AddOrUpdateAsync(PlanEntity plan);
    
    Task<bool> AddOrUpdateBulk(IEnumerable<PlanEntity> plans);
    
    Task<PlanEntity> GetAsync(string id);
    
    Task<List<PlanEntity>?> GetAllAsync();
    
    Task<bool> RemoveAsync(string id);

    Task<long?> RemoveAll();

    Task<List<PlanEntity>> SearchPlansAsync(string query);

    Task<List<PlanEntity>> SearchThroughMyPlans(string query, Guid userId);

    Task<List<PlanEntity>> SearchThroughFavoritePlans(string query, List<Guid> favoritePlanIds);

    Task<List<PlanEntity>> SearchThroughCompletedPlans(string query, List<Guid> completedPlanIds);

}