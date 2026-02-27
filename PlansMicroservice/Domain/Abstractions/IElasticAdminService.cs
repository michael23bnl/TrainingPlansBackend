using Shared.DTO;
using TrainingPlans.API.DTO;
using TrainingPlans.Domain.Entities;

namespace TrainingPlans.Domain.Abstractions;

public interface IElasticAdminService
{
    Task CreateIndexAsync(CancellationToken ct);
    Task<bool> ContainsDocumentsAsync(CancellationToken ct);
    Task<bool> IndexPlanAsync(PlanIndexRequest request, CancellationToken ct);
    Task<bool> IndexCustomPlanAsync(CustomPlanIndexRequest request, CancellationToken ct);
    Task<bool> IndexPlansAsync(List<PlanIndexRequest> request, CancellationToken ct);
    Task<bool> IndexPlansAsync(List<PlanEntity> plans, CancellationToken ct);
    Task<bool> IndexCustomPlansAsync(List<CustomPlanIndexRequest> request, CancellationToken ct);
    Task<PlanResponse?> GetAsync(Guid id, CancellationToken ct);
    Task<List<PlanResponse>?> GetAllAsync(CancellationToken ct);
    Task<bool> RemoveAsync(Guid id, CancellationToken ct);
    Task<long?> RemoveAllAsync(CancellationToken ct);
}