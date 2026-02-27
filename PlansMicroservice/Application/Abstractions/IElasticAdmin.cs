using TrainingPlans.Application.Models;
using TrainingPlans.Domain.Entities;

namespace TrainingPlans.Application.Abstractions;

public interface IElasticAdmin
{
    Task CreateIndexAsync(CancellationToken ct);
    Task<bool> ContainsDocumentsAsync(CancellationToken ct);
    Task<bool> AddOrUpdatePlanAsync(PlanEntity plan, CancellationToken ct);
    Task<bool> AddOrUpdateCustomPlanAsync(CustomPlanEntity plan, CancellationToken ct);
    Task<bool> AddOrUpdatePlanBulkAsync(List<PlanEntity> plans, CancellationToken ct);
    Task<bool> AddOrUpdateCustomPlanBulkAsync(List<CustomPlanEntity> plans, CancellationToken ct);
    Task<PlanSearchResult?> GetAsync(Guid id, CancellationToken ct);
    Task<List<PlanSearchResult>?> GetAllAsync(CancellationToken ct);
    Task<bool> RemoveAsync(Guid id, CancellationToken ct);
    Task<long?> RemoveAllAsync(CancellationToken ct);
}