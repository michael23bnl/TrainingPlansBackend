using TrainingPlans.Domain.Entities;
using Shared.Pagination;

namespace TrainingPlans.Application.Abstractions;

public interface IElasticService
{
    Task CreateIndexIfNotExistsAsync(string indexName, CancellationToken ct);

    Task<bool> ContainsDocumentsAsync(string indexName, CancellationToken ct);

    Task<bool> AddOrUpdateAsync(PlanEntity plan, CancellationToken ct);

    Task<bool> AddOrUpdateBulkAsync(CancellationToken ct);
    
    Task<PlanEntity> GetAsync(string id, CancellationToken ct);
    
    Task<List<PlanEntity>?> GetAllAsync(CancellationToken ct);
    
    Task<bool> RemoveAsync(string id, CancellationToken ct);

    Task<long?> RemoveAllAsync(CancellationToken ct);

    Task<(int totalCount, List<PlanEntity> plans)> SearchPlansAsync(string query, PlanParameters planParameters, CancellationToken ct);

    Task<(int totalCount, List<PlanEntity> plans)> SearchThroughCustomPlansAsync(string query, Guid userId,
        PlanParameters planParameters, CancellationToken ct);

    Task<(int totalCount, List<PlanEntity> plans)> SearchThroughCompletedPlansAsync(string query, Guid userId,
        PlanParameters planParameters, CancellationToken ct);
}