using Elastic.Clients.Elasticsearch;
using Elastic.Clients.Elasticsearch.IndexManagement;
using Elastic.Clients.Elasticsearch.Mapping;
using Elastic.Clients.Elasticsearch.QueryDsl;
using Microsoft.Extensions.Options;
using TrainingPlans.Configurations;
using TrainingPlans.Entities;
using TrainingPlans.Pagination;

namespace TrainingPlans.Services;

public class ElasticService : IElasticService
{
    
    private readonly ElasticsearchClient _client;
    private readonly ElasticSettings _settings;

    public ElasticService(IOptions<ElasticSettings> settings)
    {
        _settings = settings.Value;
        
        var elasticSettings = new ElasticsearchClientSettings(new Uri(_settings.Url))
            // .Authentication()
            .DefaultIndex(_settings.DefaultIndex);
        
        _client = new ElasticsearchClient(elasticSettings);
    }

    public async Task CreateIndexIfNotExistsAsync(string indexName)
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
            });
        }
    }


    public async Task<bool> AddOrUpdateAsync(PlanEntity plan)
    {
        var response = await _client.IndexAsync(plan, idx
            => idx.Index(_settings.DefaultIndex)
                .OpType(OpType.Index));
        
        return response.IsValidResponse;
    }

    public async Task<bool> AddOrUpdateBulk(IEnumerable<PlanEntity> plans)
    {
        var response = await _client.BulkAsync(
            b => b.Index(_settings.DefaultIndex)
            .UpdateMany(plans,
                (pd, p) 
                    => pd.Doc(p).DocAsUpsert(true)));
        
        return response.IsValidResponse;
    }
    
    /*public async Task<bool> AddOrUpdateBulk(IEnumerable<PlanEntity> plans)
    {
        var sortedPlans = plans.Select(plan =>
        {
            plan.Exercises = plan.Exercises
                .OrderBy(e => e.CreatedAt)
                .ToList();
            return plan;
        });

        var response = await _client.BulkAsync(
            b => b.Index(_settings.DefaultIndex)
                .UpdateMany(sortedPlans,
                    (pd, p) 
                        => pd.Doc(p).DocAsUpsert(true)));
    
        return response.IsValidResponse;
    }*/
    
    /*public async Task<List<PlanEntity>> SearchPlansAsync(string query)
    {
        var response = await _client.SearchAsync<PlanEntity>(s => s
            .Query(q => q
                .Bool(b => b
                    .Should(
                        // Поиск по названию плана
                        q => q.Match(m => m
                                .Field(p => p.Category)
                                .Query(query)
                                .Fuzziness(new Fuzziness("AUTO")) 
                        ),
                    
                        // Поиск по названиям упражнений
                        q => q.Nested(n => n
                            .Path(p => p.Exercises)
                            .Query(nq => nq
                                .Match(m => m
                                    .Field("exercises.name")
                                    .Query(query)
                                    .Fuzziness(new Fuzziness("AUTO"))
                                )
                            )
                        )
                    )
                )
                
            ).Size(10));
        return response.Documents.ToList();
    }*/
    
    public async Task<(int totalCount, List<PlanEntity> plans)> SearchPlansAsync(string query, PlanParameters planParameters)
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
                            .Field(f => f.Category)
                            .Query(query)
                            .Fuzziness(new Fuzziness("AUTO"))
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
    
    public async Task<(int totalCount, List<PlanEntity> plans)> SearchThroughMyPlans(string query, Guid userId, PlanParameters planParameters)
    {
        var response = await _client.SearchAsync<PlanEntity>(s => s
            .Query(q => q
                .Bool(b => b
                    .Must(
                        q=> q.Term(t => t
                            .Field(f => f.CreatedBy).Value(userId.ToString())),
                        q => q.Bool(b2 => b2
                            .Should(
                                // Поиск по названиям упражнений
                                q => q.Match(m => m
                                    .Field("exercises.name")
                                    .Query(query)
                                    .Fuzziness(new Fuzziness("AUTO"))),
                                // Поиск по категории
                                q => q.Match(m => m
                                    .Field(f => f.Category)
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

    public async Task<(int totalCount, List<PlanEntity> plans)> SearchThroughFavoritePlans(string query, List<Guid> favoritePlanIds, PlanParameters planParameters)
    {

        if (favoritePlanIds == null || !favoritePlanIds.Any())
        {
            return (0, new List<PlanEntity>());
        }
        
        var ids = favoritePlanIds.Select(id => id.ToString("D")).ToList();

        var response = await _client.SearchAsync<PlanEntity>(s => s
            .Query(q => q
                .Bool(b => b
                    .Must(
                        
                        q => q.Ids(new IdsQuery
                        {
                            Values = new Ids(favoritePlanIds.Select(id => id.ToString()))
                        }),
                        q => q.Bool(b2 => b2
                            .Should(
                                q => q.Match(m => m
                                    .Field("exercises.name")
                                    .Query(query)
                                    .Fuzziness(new Fuzziness("AUTO"))),
                                q => q.Match(m => m
                                    .Field(f => f.Category)
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
    
    public async Task<(int totalCount, List<PlanEntity> plans)> SearchThroughCompletedPlans(string query, List<Guid> completedPlanIds, PlanParameters planParameters)
    {

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
                                    .Field(f => f.Category)
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

    public async Task<PlanEntity> GetAsync(string id)
    {
        var response = await _client.GetAsync<PlanEntity>(id, g =>
            g.Index(_settings.DefaultIndex));
        
        return response.Source;
    }

    public async Task<List<PlanEntity>?> GetAllAsync()
    {
        var response = await _client.SearchAsync<PlanEntity>(s =>
            s.Index(_settings.DefaultIndex));
        
        return response.IsValidResponse ? response.Documents.ToList() : default;
    }

    public async Task<bool> RemoveAsync(string id)
    {
        var response = await _client.DeleteAsync<PlanEntity>(id,
            d => d.Index(_settings.DefaultIndex));
        
        return response.IsValidResponse;
    }

    public async Task<long?> RemoveAll()
    {
        var response = await _client.DeleteByQueryAsync<PlanEntity>(d => d
            .Indices(_settings.DefaultIndex)
            .Query(q => q
                    .MatchAll(new MatchAllQuery())
            )
        );
        
        return response.IsValidResponse ? response.Deleted : default;
    }
}