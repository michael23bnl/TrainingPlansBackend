using Shared.DTO;
using TrainingPlans.Domain.Entities;
using Shared.Pagination;

namespace TrainingPlans.Domain.Abstractions;

public interface IPlansService
{
    Task<Guid> CreatePlanAsync(List<Guid> exerciseIds, Guid? userId, CancellationToken ct);
    Task<(int, List<PlanResponse>)> GetAllPreloadedPlansAsync(PlanParameters planParameters, CancellationToken ct);
    Task<List<PlanEntity>> GetAllPreloadedPlansAsync(CancellationToken ct);
    Task<PlanResponse?> GetPlanAsync(Guid planId, Guid userId, CancellationToken ct);
    Task<List<PlanEntity>> GetPlansAsync(List<Guid> planIds, CancellationToken ct);
    Task<Guid> UpdatePlanAsync(Guid id, List<Guid> exerciseIds, CancellationToken ct);
    Task<Guid> DeletePlanAsync(Guid id, CancellationToken ct);
}