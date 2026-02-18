using Shared.Pagination;
using TrainingPlans.Application.Abstractions;
using TrainingPlans.Domain.Abstractions;
using TrainingPlans.Infrastructure.Elasticsearch.Models;

namespace TrainingPlans.Application.Services;

public class PlansSearchService : IPlansSearchService
{
    private readonly IFullTextSearch _fullTextSearch;

    public PlansSearchService(IFullTextSearch fullTextSearch)
    {
        _fullTextSearch = fullTextSearch;
    }
    
    public async Task<(int totalCount, List<PlanSearchDocument> plans)> SearchPlansAsync(
        string query, PlanParameters parameters, CancellationToken ct)
    {
        return await _fullTextSearch.SearchAsync(query, parameters, ct);
    }
}