using TrainingPlans.Domain.Entities;
using Shared.Pagination;
using TrainingPlans.Domain.DTO;

namespace TrainingPlans.Domain.Abstractions;

public interface IPlansRepository
{
    Task<Guid> CreateAsync(List<PlanExercise> exercises, string? description, CancellationToken ct);
    Task<(int, List<PlanEntity>)> GetAllAsync(
        PlanParameters? planParameters, CancellationToken ct);
    Task<PlanEntity?> GetAsync(Guid planId, CancellationToken ct);
    Task<List<PlanEntity>> GetByIdsAsync(List<Guid> planIds, CancellationToken ct);
    Task<Guid> UpdateAsync(Guid id, List<PlanExercise>? exercises, string? description,
        CancellationToken ct);
    Task<Guid> DeleteAsync(Guid id, CancellationToken ct);
}