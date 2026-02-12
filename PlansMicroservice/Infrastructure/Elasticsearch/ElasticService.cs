
using Elastic.Clients.Elasticsearch;
using Elastic.Clients.Elasticsearch.IndexManagement;
using Elastic.Clients.Elasticsearch.Mapping;
using Elastic.Clients.Elasticsearch.QueryDsl;
using Microsoft.Extensions.Options;
using TrainingPlans.Domain.Entities;
using Shared.Pagination;
using TrainingPlans.Application.Abstractions;
using TrainingPlans.Domain.Abstractions;

namespace TrainingPlans.Infrastructure.Elasticsearch;
/*
public class ElasticService : IElasticService
{
    private readonly ElasticsearchClient _client;
    private readonly ElasticSettings _settings;
    private readonly IPlansService _plansService;
    private readonly ICustomPlansService _customPlansService;

    public ElasticService(IOptions<ElasticSettings> settings, IPlansService plansService, 
        ICustomPlansService customPlansService)
    {
        _settings = settings.Value;
        
        var elasticSettings = new ElasticsearchClientSettings(new Uri(_settings.Url))
            // .Authentication()
            .DefaultIndex(_settings.DefaultIndex);
        
        _client = new ElasticsearchClient(elasticSettings);
        _plansService = plansService;
        _customPlansService = customPlansService;
    }

    public async Task CreateIndexIfNotExistsAsync(string indexName, CancellationToken ct)
    {
        if (!_client.Indices.Exists(indexName).Exists)
        {
            var response = await _client.Indices.CreateAsync(new CreateIndexRequest(indexName)
            {
                Mappings = new TypeMapping
                {
                    Properties = new Properties(new Dictionary<PropertyName, IProperty>
                    {
                        { "id", new KeywordProperty() { IgnoreAbove = 256 } },

                        {
                            "name", new TextProperty()
                            {
                                Fields = new Properties
                                {
                                    { "keyword", new KeywordProperty() { IgnoreAbove = 256 } }
                                }
                            }
                        },

                        { "createdBy", new KeywordProperty() { IgnoreAbove = 256 } },

                        {
                            "exercises", new ObjectProperty()
                            {
                                Properties = new Properties(new Dictionary<PropertyName, IProperty>
                                {
                                    { "id", new KeywordProperty() { IgnoreAbove = 256 } },

                                    {
                                        "name", new TextProperty()
                                        {
                                            Fields = new Properties
                                            {
                                                { "keyword", new KeywordProperty() { IgnoreAbove = 256 } }
                                            }
                                        }
                                    },

                                    { "muscleGroup", new KeywordProperty() { IgnoreAbove = 256 } },
                                })
                            }
                        }
                    })
                }
            }, ct);
        }
    }
    
    public async Task<bool> ContainsDocumentsAsync(string indexName, CancellationToken ct)
    {
        var response = await _client.CountAsync<PlanEntity>(c => c
            .Indices(indexName), ct
        );

        return response.Count > 0;
    }


    public async Task<bool> AddOrUpdateAsync(PlanEntity plan, CancellationToken ct)
    {
        var response = await _client.IndexAsync(plan, idx
            => idx.Index(_settings.DefaultIndex)
                .OpType(OpType.Index), ct);
        
        return response.IsValidResponse;
    }

    public async Task<bool> AddOrUpdateBulkAsync(CancellationToken ct)
    {
        var plans = await _plansService.GetAllPreloadedPlansAsync(ct);
        var response = await _client.BulkAsync(
            b => b.Index(_settings.DefaultIndex)
            .UpdateMany(plans,
                (pd, p) 
                    => pd.Doc(p).DocAsUpsert(true)), ct);
        
        return response.IsValidResponse;
    }
    
    public async Task<(int totalCount, List<PlanEntity> plans)> SearchPlansAsync(string query,
        PlanParameters planParameters, CancellationToken ct)
    {
        var response = await _client.SearchAsync<PlanEntity>(s => s
            .Query(q => q
                .Bool(b => b
                    .Should(
                        q => q.Match(m => m
                            .Field("exercises.name")
                            .Query(query)
                            .Fuzziness(new Fuzziness("AUTO"))
                        ),
                        q => q.Match(m => m
                            .Field(f => f.Tags)
                            .Query(query)
                            .Fuzziness(new Fuzziness("AUTO"))
                        )
                    )
                )
            )
            .From((planParameters.PageNumber - 1) * planParameters.PageSize)
            .Size(planParameters.PageSize), ct
        );

        var totalCount = (int)response.Total;
        var plans = response.Documents.ToList();
        //return response.Documents.ToList();
        return (totalCount, plans);
    }

    public async Task<(int totalCount, List<PlanEntity> plans)> SearchThroughCustomPlansAsync(string query,
        Guid userId, PlanParameters planParameters, CancellationToken ct)
    {
        var response = await _client.SearchAsync<PlanEntity>(s => s
            .Query(q => q
                .Bool(b => b
                    .Must(
                        q=> q.Term(t => t
                            .Field(f => f.CreatedBy).Value(userId.ToString())),
                        q => q.Bool(b2 => b2
                            .Should(
                                // поиск по названиям упражнений
                                q => q.Match(m => m
                                    .Field("exercises.name")
                                    .Query(query)
                                    .Fuzziness(new Fuzziness("AUTO"))),
                                // поиск по категории
                                q => q.Match(m => m
                                    .Field(f => f.Tags)
                                    .Query(query)
                                    .Fuzziness(new Fuzziness("AUTO"))
                                )
                            )
                        )
                    )
                )
            )
            .From((planParameters.PageNumber - 1) * planParameters.PageSize)
            .Size(planParameters.PageSize), ct
        );

        var totalCount = (int)response.Total;
        var plans = response.Documents.ToList();
        //return response.Documents.ToList();
        return (totalCount, plans);
    }

    public async Task<(int totalCount, List<PlanEntity> plans)> SearchThroughCompletedPlansAsync(string query,
        Guid userId, PlanParameters planParameters, CancellationToken ct)
    {
        var completedPlanIds = await _customPlansService.GetCompletedPlansIdsAsync(userId, ct);
        
        if (completedPlanIds == null || !completedPlanIds.Any())
        {
            return (0, new List<PlanEntity>());
        }
        
        var ids = completedPlanIds.Select(id => id.ToString("D")).ToList();

        var response = await _client.SearchAsync<PlanEntity>(s => s
            .Query(q => q
                .Bool(b => b
                    .Must(
                        
                        q => q.Ids(new IdsQuery
                        {
                            Values = new Ids(completedPlanIds.Select(id => id.ToString()))
                        }),
                        q => q.Bool(b2 => b2
                            .Should(
                                q => q.Match(m => m
                                    .Field("exercises.name")
                                    .Query(query)
                                    .Fuzziness(new Fuzziness("AUTO"))),
                                q => q.Match(m => m
                                    .Field(f => f.Tags)
                                    .Query(query)
                                    .Fuzziness(new Fuzziness("AUTO"))
                                )
                            )
                        )
                    )
                )
            )
            .From((planParameters.PageNumber - 1) * planParameters.PageSize)
            .Size(planParameters.PageSize)
        );

        var totalCount = (int)response.Total;
        var plans = response.Documents.ToList();
        //return response.Documents.ToList();
        return (totalCount, plans);
    }

    public async Task<PlanEntity> GetAsync(string id, CancellationToken ct)
    {
        var response = await _client.GetAsync<PlanEntity>(id, g =>
            g.Index(_settings.DefaultIndex), ct);
        
        return response.Source;
    }

    public async Task<List<PlanEntity>?> GetAllAsync(CancellationToken ct)
    {
        var response = await _client.SearchAsync<PlanEntity>(s =>
            s.Index(_settings.DefaultIndex), ct);
        
        return response.IsValidResponse ? response.Documents.ToList() : default;
    }

    public async Task<bool> RemoveAsync(string id, CancellationToken ct)
    {
        var response = await _client.DeleteAsync<PlanEntity>(id,
            d => d.Index(_settings.DefaultIndex), ct);
        
        return response.IsValidResponse;
    }

    public async Task<long?> RemoveAllAsync(CancellationToken ct)
    {
        var response = await _client.DeleteByQueryAsync<PlanEntity>(d => d
            .Indices(_settings.DefaultIndex)
            .Query(q => q
                    .MatchAll(new MatchAllQuery())
            ), ct
        );
        
        return response.IsValidResponse ? response.Deleted : default;
    }
} */