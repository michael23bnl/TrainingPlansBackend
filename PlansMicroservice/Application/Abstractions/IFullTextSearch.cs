using TrainingPlans.Domain.Entities;
using Shared.Pagination;
using TrainingPlans.Infrastructure.Elasticsearch.Models;

namespace TrainingPlans.Application.Abstractions;

public interface IFullTextSearch
{
    Task<(int totalCount, List<PlanSearchDocument> plans)> SearchAsync(string query, Guid? userId, PlanParameters planParameters,
        CancellationToken ct);
}