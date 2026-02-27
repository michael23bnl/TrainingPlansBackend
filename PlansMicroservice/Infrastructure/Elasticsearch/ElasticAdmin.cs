
using Elastic.Clients.Elasticsearch;
using Elastic.Clients.Elasticsearch.QueryDsl;
using TrainingPlans.Domain.Entities;
using TrainingPlans.Infrastructure.Elasticsearch.ElasticClient;
using TrainingPlans.Infrastructure.Elasticsearch.Models;

namespace TrainingPlans.Infrastructure.Elasticsearch;

public class ElasticAdmin : IElasticAdmin
{
    private readonly ElasticsearchClient _client;
    private readonly ElasticSettings _settings;

    public ElasticAdmin(IElasticClientProvider elasticClientProvider)
    {
        _client = elasticClientProvider.Client;
        _settings = elasticClientProvider.Settings;
    }
    
    public async Task CreateIndexIfNotExistsAsync(CancellationToken ct)
    {
        var exists = await _client.Indices.ExistsAsync(_settings.DefaultIndex, ct);
        
        if (exists.Exists) //return;
            await _client.Indices.DeleteAsync(_settings.DefaultIndex, ct);

        await _client.Indices.CreateAsync(_settings.DefaultIndex, c => c
            .Mappings(m => m
                .Properties<PlanSearchDocument>(p => p
                    .Keyword(ps => ps.Id)
                    .Keyword(ps => ps.UserId)  
                    .Text(ps => ps.Description)
                    .Date(ps => ps.CreatedAt)
                    .Date(ps => ps.CompletionDate, dp => dp
                        .Index(false))
                    .Text(ps => ps.Tags)
                    .Nested(ps => ps.Exercises, np => np
                        .Properties(pd => pd
                            .Keyword("id")
                            .Text("name")
                            .Text("description") 
                            .Text("muscleGroup", tp => tp
                                .Index(false))
                            .IntegerNumber("sets", tp => tp
                                .Index(false)
                            )
                            .IntegerNumber("reps", tp => tp
                                .Index(false)
                            )
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

    public async Task<bool> AddOrUpdateAsync<TPlan>(
        TPlan plan, 
        Func<TPlan, PlanSearchDocument> map,
        CancellationToken ct)
    {
        var doc = map(plan);
        var response = await _client.IndexAsync(doc, idx => idx
                .Index(_settings.DefaultIndex)
                .OpType(OpType.Index), ct);
        return response.IsValidResponse;
    }

    public async Task<bool> AddOrUpdateBulkAsync<TPlan>(
        List<TPlan> plans, 
        Func<TPlan, PlanSearchDocument> map,
        CancellationToken ct)
    {
        var docs = plans
            .Select(p => map(p))
            .ToList();
        var response = await _client.BulkAsync(b => b
            .Index(_settings.DefaultIndex)
            .UpdateMany(docs, (pd, p) => pd
                .Doc(p)
                .DocAsUpsert(true)), ct);
        
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
    
    // private static PlanSearchDocument Map(PlanEntity plan)
    // {
    //     return new PlanSearchDocument
    //     {
    //         Id = plan.Id,
    //         Description = plan.Description,
    //         CreatedAt = plan.CreatedAt,
    //         Tags = plan.PlanExercises
    //             .SelectMany(pe => pe.Exercise.MuscleGroup
    //                 .Split(',', StringSplitOptions.RemoveEmptyEntries)
    //                 .Select(tag => tag.Trim()))
    //             .Distinct()
    //             .ToList(),
    //         Exercises = plan.PlanExercises
    //             .Select(pe => new ExerciseSearchDocument
    //             {
    //                 Id = pe.Exercise.Id,
    //                 Name = pe.Exercise.Name,
    //                 MuscleGroup = pe.Exercise.MuscleGroup,
    //                 Description = pe.Exercise.Description,
    //                 Sets = pe.Sets,
    //                 Reps = pe.Reps
    //             })
    //             .ToList(),
    //         UserId = null,
    //         CompletionDate = null
    //     };
    // }
    //
    // private static PlanSearchDocument Map(CustomPlanEntity customPlan)
    // {
    //     return new PlanSearchDocument
    //     {
    //         Id = customPlan.Id,
    //         Description = customPlan.Description,
    //         CreatedAt = customPlan.CreatedAt,
    //         Tags = customPlan.PlanExercises
    //             .SelectMany(cpe => cpe.Exercise.MuscleGroup
    //                 .Split(',', StringSplitOptions.RemoveEmptyEntries)
    //                 .Select(tag => tag.Trim()))
    //             .Distinct()
    //             .ToList(),
    //         Exercises = customPlan.PlanExercises
    //             .Select(cpe => new ExerciseSearchDocument
    //             {
    //                 Id = cpe.Exercise.Id,
    //                 Name = cpe.Exercise.Name,
    //                 MuscleGroup = cpe.Exercise.MuscleGroup,
    //                 Description = cpe.Exercise.Description,
    //                 Sets = cpe.Sets,
    //                 Reps = cpe.Reps,
    //                 Notes = cpe.Notes
    //             })
    //             .ToList(),
    //         UserId = customPlan.UserId,
    //         CompletionDate = customPlan.CompletionDate
    //     };
    // }

    private static PlanSearchDocument Map<TPlan, TPlanExercise>(
        TPlan plan,
        Func<TPlan, IEnumerable<TPlanExercise>> getPlanExercises,
        Func<TPlanExercise, ExerciseSearchDocument> mapExercise,
        Func<TPlan, Guid?> getUserId,
        Func<TPlan, DateOnly?> getCompletionDate)
    {
        return new PlanSearchDocument
        {
            Id = (Guid)typeof(TPlan).GetProperty("Id")!.GetValue(plan)!,
            Description = (string?)typeof(TPlan).GetProperty("Description")!.GetValue(plan),
            CreatedAt = (DateTime)typeof(TPlan).GetProperty("CreatedAt")!.GetValue(plan)!,
            Tags = getPlanExercises(plan)
                .SelectMany(pe =>
                {
                    var exercise = typeof(TPlanExercise).GetProperty("Exercise")!.GetValue(pe);
                    if (exercise == null) return Array.Empty<string>();
                    var muscleGroup = (string?)exercise.GetType().GetProperty("MuscleGroup")!.GetValue(exercise);
                    return muscleGroup?.Split(',', StringSplitOptions.RemoveEmptyEntries)
                        .Select(tag => tag.Trim()) ?? Array.Empty<string>();
                })
                .Distinct()
                .ToList(),
            Exercises = getPlanExercises(plan)
                .Select(pe => mapExercise(pe))
                .ToList(),
            UserId = getUserId(plan),
            CompletionDate = getCompletionDate(plan)
        };
    }

    private static PlanSearchDocument MapPlan(PlanEntity plan) =>
        Map(
            plan,
            p => p.PlanExercises,
            pe => new ExerciseSearchDocument
            {
                Id = pe.Exercise.Id,
                Name = pe.Exercise.Name,
                MuscleGroup = pe.Exercise.MuscleGroup,
                Description = pe.Exercise.Description,
                Sets = pe.Sets,
                Reps = pe.Reps
            },
            _ => null,
            _ => null
        );
    
    private static PlanSearchDocument MapCustomPlan(CustomPlanEntity customPlan) =>
        Map(
            customPlan,
            cp => cp.PlanExercises,
            cpe => new ExerciseSearchDocument
            {
                Id = cpe.Exercise.Id,
                Name = cpe.Exercise.Name,
                MuscleGroup = cpe.Exercise.MuscleGroup,
                Description = cpe.Exercise.Description,
                Sets = cpe.Sets,
                Reps = cpe.Reps,
                Notes = cpe.Notes
            },
            cp => cp.UserId,
            cp => cp.CompletionDate
        );
} 