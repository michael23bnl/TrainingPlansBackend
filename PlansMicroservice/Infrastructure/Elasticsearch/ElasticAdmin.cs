
using Elastic.Clients.Elasticsearch;
using Elastic.Clients.Elasticsearch.QueryDsl;
using Microsoft.Extensions.Options;
using TrainingPlans.Domain.Entities;
using TrainingPlans.Infrastructure.Elasticsearch.Models;

namespace TrainingPlans.Infrastructure.Elasticsearch;

public class ElasticAdmin : IElasticAdmin
{
    private readonly ElasticsearchClient _client;
    private readonly ElasticSettings _settings;

    public ElasticAdmin(IOptions<ElasticSettings> settings)
    {
        _settings = settings.Value;
        
        var elasticSettings = new ElasticsearchClientSettings(new Uri(_settings.Url))
            .DefaultIndex(_settings.DefaultIndex);
        
        _client = new ElasticsearchClient(elasticSettings);
    }
    
    public async Task CreateIndexIfNotExistsAsync(CancellationToken ct)
    {
        var exists = await _client.Indices.ExistsAsync(_settings.DefaultIndex, ct);

        if (exists.Exists) //return;
            await _client.Indices.DeleteAsync(_settings.DefaultIndex, ct);

        await _client.Indices.CreateAsync(_settings.DefaultIndex, c => c
            .Mappings(m => m
                .Properties<PlanSearchDocument>(p => p
                    .Keyword(k => k.Id)
                    .Text(t => t.Description)
                    .Date(d => d.CreatedAt)
                    .Text(t => t.Tags)
                    .Nested(n => n.Exercises, np => np
                        .Properties(pp => pp
                            .Keyword("id")
                            .Text("name")
                            .Keyword("muscleGroup")
                        )
                    )
                )
            ), ct);
    }
    
    public async Task<bool> ContainsDocumentsAsync(CancellationToken ct)
    {
        var response = await _client.CountAsync<PlanSearchDocument>(c => c
            .Indices(_settings.DefaultIndex), ct
        );

        return response.Count > 0;
    }

    public async Task<bool> AddOrUpdateAsync(PlanEntity plan, CancellationToken ct)
    {
        var doc = Map(plan);
        var response = await _client.IndexAsync(doc, idx => idx
                .Index(_settings.DefaultIndex)
                .OpType(OpType.Index), ct);
        
        return response.IsValidResponse;
    }

    public async Task<bool> AddOrUpdateBulkAsync(List<PlanEntity> plans, CancellationToken ct)
    {
        var docs = plans
            .Select(p => Map(p))
            .ToList();
        var response = await _client.BulkAsync(
            b => b
                .Index(_settings.DefaultIndex)
            .UpdateMany(docs,
                (pd, p) 
                    => pd.Doc(p).DocAsUpsert(true)), ct);
        
        return response.IsValidResponse;
    }

    public async Task<PlanSearchDocument?> GetAsync(string id, CancellationToken ct)
    {
        var response = await _client.GetAsync<PlanSearchDocument>(
            id, 
            g => g.Index(_settings.DefaultIndex), 
            ct);
        
        return response.Source;
    }

    public async Task<List<PlanSearchDocument>?> GetAllAsync(CancellationToken ct)
    {
        var response = await _client.SearchAsync<PlanSearchDocument>(s =>
            s.Index(_settings.DefaultIndex), ct);
        
        return response.IsValidResponse ? response.Documents.ToList() : null;
    }

    public async Task<bool> RemoveAsync(string id, CancellationToken ct)
    {
        var response = await _client.DeleteAsync<PlanSearchDocument>(
            id,
            d => d.Index(_settings.DefaultIndex), 
            ct);
        
        return response.IsValidResponse;
    }

    public async Task<long?> RemoveAllAsync(CancellationToken ct)
    {
        var response = await _client.DeleteByQueryAsync<PlanSearchDocument>(d => d
            .Indices(_settings.DefaultIndex)
            .Query(q => q
                    .MatchAll(new MatchAllQuery())
            ), ct);
        
        return response.IsValidResponse ? response.Deleted : null;
    }
    
    private static PlanSearchDocument Map(PlanEntity plan)
    {
        return new PlanSearchDocument
        {
            Id = plan.Id,
            Description = plan.Description,
            CreatedAt = plan.CreatedAt,
            Tags = plan.PlanExercises
                .SelectMany(pe => pe.Exercise.MuscleGroup
                    .Split(',', StringSplitOptions.RemoveEmptyEntries)
                    .Select(tag => tag.Trim()))
                .Distinct()
                .ToList(),
            Exercises = plan.PlanExercises
                .Select(pe => new ExerciseSearchDocument
                {
                    Id = pe.Exercise.Id.ToString(),
                    Name = pe.Exercise.Name,
                    MuscleGroup = pe.Exercise.MuscleGroup
                })
                .ToList()
        };
    }
} 