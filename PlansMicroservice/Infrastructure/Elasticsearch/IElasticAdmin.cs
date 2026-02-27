using TrainingPlans.Domain.Entities;
using TrainingPlans.Infrastructure.Elasticsearch.Models;

namespace TrainingPlans.Infrastructure.Elasticsearch;

public interface IElasticAdmin
{
    Task CreateIndexIfNotExistsAsync(CancellationToken ct);
    Task<bool> ContainsDocumentsAsync(CancellationToken ct);
    Task<bool> AddOrUpdateAsync<TPlan>(TPlan plan, Func<TPlan, PlanSearchDocument> map, CancellationToken ct);
    Task<bool> AddOrUpdateBulkAsync<TPlan>(List<TPlan> plans, Func<TPlan, PlanSearchDocument> map,
        CancellationToken ct);
    Task<PlanSearchDocument?> GetAsync(string id, CancellationToken ct);
    Task<List<PlanSearchDocument>?> GetAllAsync(CancellationToken ct);
    Task<bool> RemoveAsync(string id, CancellationToken ct);
    Task<long?> RemoveAllAsync(CancellationToken ct);
}