using Elastic.Clients.Elasticsearch;
using Elastic.Clients.Elasticsearch.IndexManagement;
using Elastic.Clients.Elasticsearch.Mapping;
using Elastic.Clients.Elasticsearch.QueryDsl;
using Microsoft.Extensions.Options;
using TrainingPlans.Configurations;
using TrainingPlans.Entities;

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
                        // ID плана
                        { "id", new KeywordProperty() { IgnoreAbove = 256 } },

                        // название плана
                        {
                            "name", new TextProperty()
                            {
                                Fields = new Properties
                                {
                                    { "keyword", new KeywordProperty() { IgnoreAbove = 256 } }
                                }
                            }
                        },

                        // ID создателя
                        { "createdBy", new KeywordProperty() { IgnoreAbove = 256 } },

                        // упражнения (вложенный тип)
                        {
                            "exercises", new NestedProperty()
                            {
                                Properties = new Properties(new Dictionary<PropertyName, IProperty>
                                {
                                    // ID упражнения
                                    { "id", new KeywordProperty() { IgnoreAbove = 256 } },

                                    // название упражнения
                                    {
                                        "name", new TextProperty()
                                        {
                                            Fields = new Properties
                                            {
                                                { "keyword", new KeywordProperty() { IgnoreAbove = 256 } }
                                            }
                                        }
                                    },

                                    // группа мышц
                                    { "muscleGroup", new KeywordProperty() { IgnoreAbove = 256 } },

                                    // ID создателя упражнения
                                    { "createdBy", new KeywordProperty() { IgnoreAbove = 256 } },

                                    // дата создания
                                    { "createdAt", new DateProperty() }
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
    
    public async Task<List<PlanEntity>> SearchPlansAsync(string query)
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