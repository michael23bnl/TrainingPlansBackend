namespace TrainingPlans.Application.Abstractions;

public interface ICustomPlansService
{
    Task<Guid> AddOrRemoveCustomPlanAsync(Guid userId, Guid planId, CancellationToken ct);
    Task<Guid> CompleteCustomPlanAsync(Guid userId, Guid planId, CancellationToken ct);
    Task<List<Guid>> GetCompletedPlansIdsAsync(Guid userId, CancellationToken ct);
}