using Shared.DTO;
using TrainingPlans.Domain.DTO;

namespace TrainingPlans.Domain.Abstractions;

public interface ICustomPlansService
{
    Task<Guid> CreateCustomPlanAsync(Guid userId, string? description, Guid? sourcePlanId,
        List<CustomPlanExercise> exercises, CancellationToken ct);
    Task<List<PlanResponse>> GetAllCustomPlansAsync(Guid userId, CancellationToken ct);
    Task<PlanResponse?> GetPlanAsync(Guid userId, Guid planId, CancellationToken ct);
    Task<List<PlanResponse>> GetCompletedPlansAsync(Guid userId, CancellationToken ct);
    Task<Guid> UpdateCustomPlanAsync(Guid userId, Guid planId, string? description,
        List<CustomPlanExercise>? exercises, CancellationToken ct);
    Task<Guid> DeleteCustomPlanAsync(Guid userId, Guid planId, CancellationToken ct);
    Task<Guid> CompleteCustomPlanAsync(Guid userId, Guid planId, CancellationToken ct);
    Task<Guid> UncompleteCustomPlanAsync(Guid userId, Guid planId, CancellationToken ct);
}