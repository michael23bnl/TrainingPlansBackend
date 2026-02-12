using TrainingPlans.Domain.DTO;
using TrainingPlans.Domain.Entities;

namespace TrainingPlans.Domain.Abstractions;

public interface ICustomPlansRepository
{
    Task<Guid> CreateAsync(Guid userId, string? description, Guid? sourcePlanId,
        List<CustomPlanExercise> exercises, CancellationToken ct);
    Task<List<CustomPlanEntity>> GetAllAsync(Guid userId, CancellationToken ct);
    Task<List<CustomPlanEntity>> GetCompletedAsync(Guid userId, CancellationToken ct);
     Task<Guid> UpdateAsync(Guid userId, Guid planId, string? description,
        List<CustomPlanExercise>? exercises, CancellationToken ct);
    Task<Guid> DeleteAsync(Guid userId, Guid planId, CancellationToken ct);
    Task<Guid> CompleteAsync(Guid userId, Guid planId, CancellationToken ct);
    Task<Guid> UncompleteAsync(Guid userId, Guid planId, CancellationToken ct);
}