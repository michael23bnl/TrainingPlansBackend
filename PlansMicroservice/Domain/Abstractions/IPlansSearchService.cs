using Shared.Pagination;
using TrainingPlans.Infrastructure.Elasticsearch.Models;

namespace TrainingPlans.Domain.Abstractions;

public interface IPlansSearchService
{
    Task<(int totalCount, List<PlanSearchDocument> plans)> SearchPlansAsync(
        string query, PlanParameters parameters, CancellationToken ct);
}