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
}