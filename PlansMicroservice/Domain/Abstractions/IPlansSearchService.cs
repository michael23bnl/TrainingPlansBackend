using Shared.DTO;
using Shared.Pagination;

namespace TrainingPlans.Domain.Abstractions;

public interface IPlansSearchService
{
    Task<(int totalCount, List<PlanResponse> plans)> SearchPlansAsync(
        string query, PlanParameters parameters, CancellationToken ct);

    Task<(int totalCount, List<PlanResponse> plans)> SearchCustomPlansAsync(
        string query, Guid userId, PlanParameters parameters, CancellationToken ct);
}