namespace TrainingPlans.Domain.Abstractions;

public interface ICustomPlansRepository
{
    Task<Guid> AddOrRemoveAsync(Guid userId, Guid planId, CancellationToken ct);
    Task<Guid> CompleteAsync(Guid userId, Guid planId, CancellationToken ct);
    Task<List<Guid>> GetCompletedIdsAsync(Guid userId, CancellationToken ct);
}