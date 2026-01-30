using TrainingPlans.Domain.Entities;
using Shared.Pagination;

namespace TrainingPlans.Domain.Abstractions;

public interface IPlansRepository
{
    Task<Guid> CreateAsync(List<Guid> exerciseIds, Guid? createdBy, CancellationToken ct);

    Task<(int, List<PlanEntity>)> GetAllPreloadedAsync(
        PlanParameters planParameters, CancellationToken ct);

    Task<List<PlanEntity>> GetAllPreloadedAsync(CancellationToken ct);

    Task<PlanEntity?> GetAsync(Guid planId, Guid? userId, CancellationToken ct);
    Task<List<PlanEntity>> GetAsync(List<Guid> planIds, CancellationToken ct);
    Task<Guid> UpdateAsync(Guid id, List<Guid> exerciseIds, CancellationToken ct);
    Task<Guid> DeleteAsync(Guid id, CancellationToken ct);
}