using Shared.Pagination;
using TrainingPlans.Application.Models;

namespace TrainingPlans.Application.Abstractions;

public interface IFullTextSearch
{
    Task<(int totalCount, List<PlanSearchResult> plans)> SearchAsync(string query, Guid? userId, PlanParameters planParameters,
        CancellationToken ct);
}