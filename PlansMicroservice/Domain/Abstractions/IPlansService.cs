using Shared.DTO;
using TrainingPlans.Domain.Entities;
using Shared.Pagination;
using TrainingPlans.Domain.DTO;

namespace TrainingPlans.Domain.Abstractions;

public interface IPlansService
{
    Task<Guid> CreatePlanAsync(List<PlanExercise> exercises, string? description, CancellationToken ct);
    Task<(int, List<PlanResponse>)> GetAllPlansAsync(PlanParameters? planParameters,
        CancellationToken ct);

    Task<List<PlanEntity>> GetAllPlansAsync(CancellationToken ct);
    Task<PlanResponse?> GetPlanAsync(Guid planId, CancellationToken ct);
    Task<List<PlanResponse>> GetPlansByIdsAsync(List<Guid> planIds, CancellationToken ct);
    Task<Guid> UpdatePlanAsync(Guid id, List<PlanExercise>? exercises, string? description, CancellationToken ct);
    Task<Guid> DeletePlanAsync(Guid id, CancellationToken ct);
}