using Elastic.Clients.Elasticsearch;
using Microsoft.Extensions.Options;
using Shared.Pagination;
using TrainingPlans.Application.Abstractions;
using TrainingPlans.Infrastructure.Elasticsearch.Models;

namespace TrainingPlans.Infrastructure.Elasticsearch;

public class FullTextSearch : IFullTextSearch
{
    private readonly ElasticsearchClient _client;
    private readonly ElasticSettings _settings;
    
    public FullTextSearch(IOptions<ElasticSettings> settings)
    {
        _settings = settings.Value;
        
        var elasticSettings = new ElasticsearchClientSettings(new Uri(_settings.Url))
            .DefaultIndex(_settings.DefaultIndex);
        
        _client = new ElasticsearchClient(elasticSettings);
    }
    
    public async Task<(int totalCount, List<PlanSearchDocument> plans)>
        SearchAsync(string query, PlanParameters parameters, CancellationToken ct)
    {
        var response = await _client.SearchAsync<PlanSearchDocument>(s => s
                .Index(_settings.DefaultIndex)
                .Query(q => q
                    .Bool(b => b
                        .Should(
                            sh => sh.Match(m => m
                                .Field(f => f.Description)
                                .Query(query)
                                .Fuzziness(new Fuzziness("AUTO"))
                                .Boost(1)
                            ),
                            sh => sh.Match(m => m
                                .Field(f => f.Tags)
                                .Query(query)
                                .Fuzziness(new Fuzziness("AUTO"))
                            ),
                            sh => sh.Nested(n => n
                                .Path(p => p.Exercises)
                                .Query(nq => nq
                                    .Match(m => m
                                        .Field("exercises.name")
                                        .Query(query)
                                        .Fuzziness(new Fuzziness("AUTO"))
                                        .Boost(3)
                                    )
                                )
                            )
                        )
                    )
                )
                .From((parameters.PageNumber - 1) * parameters.PageSize)
                .Size(parameters.PageSize),
            ct);

        return ((int)response.Total, response.Documents.ToList());
    }
}