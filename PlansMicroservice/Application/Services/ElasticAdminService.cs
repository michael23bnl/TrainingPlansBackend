using Shared.DTO;
using TrainingPlans.API.DTO;
using TrainingPlans.Application.Abstractions;
using TrainingPlans.Application.Models;
using TrainingPlans.Domain.Abstractions;
using TrainingPlans.Domain.Entities;

namespace TrainingPlans.Application.Services;

public class ElasticAdminService : IElasticAdminService
{
    private readonly IElasticAdmin _elasticAdmin;

    public ElasticAdminService(IElasticAdmin elasticAdmin)
    {
        _elasticAdmin = elasticAdmin;
    }

    public async Task CreateIndexAsync(CancellationToken ct)
    {
        await _elasticAdmin.CreateIndexAsync(ct);
    }

    public async Task<bool> ContainsDocumentsAsync(CancellationToken ct)
    {
        var contains = await _elasticAdmin.ContainsDocumentsAsync(ct);

        return contains;
    }

    public async Task<bool> IndexPlanAsync(PlanIndexRequest planRequest, CancellationToken ct)
    {
        var planEntity = Map(planRequest);
        var result = await _elasticAdmin.AddOrUpdatePlanAsync(planEntity, ct);

        return result;
    }
    
    public async Task<bool> IndexCustomPlanAsync(CustomPlanIndexRequest planRequest, CancellationToken ct)
    {
        var customPlanEntity = Map(planRequest);
        var result = await _elasticAdmin.AddOrUpdateCustomPlanAsync(customPlanEntity, ct);

        return result;
    }
    
    public async Task<bool> IndexPlansAsync(List<PlanIndexRequest> plansRequest, CancellationToken ct)
    {
        var planEntities = plansRequest
            .Select(p => Map(p))
            .ToList();
        var result = await _elasticAdmin.AddOrUpdatePlanBulkAsync(planEntities, ct);

        return result;
    }
    
    public async Task<bool> IndexPlansAsync(List<PlanEntity> plans, CancellationToken ct)
    {
        var result = await _elasticAdmin.AddOrUpdatePlanBulkAsync(plans, ct);

        return result;
    }
    
    public async Task<bool> IndexCustomPlansAsync(List<CustomPlanIndexRequest> plansRequest, CancellationToken ct)
    {
        var customPlanEntities = plansRequest
            .Select(p => Map(p))
            .ToList();
        var result = await _elasticAdmin.AddOrUpdateCustomPlanBulkAsync(customPlanEntities, ct);

        return result;
    }

    public async Task<PlanResponse?> GetAsync(Guid id, CancellationToken ct)
    {
        var doc = await _elasticAdmin.GetAsync(id, ct);
        var response = Map(doc);
        
        return response;
    }

    public async Task<List<PlanResponse>?> GetAllAsync(CancellationToken ct)
    {
        var docs = await _elasticAdmin.GetAllAsync(ct);
        var response = docs
            .Select(d => Map(d))
            .ToList();

        return response;
    }

    public async Task<bool> RemoveAsync(Guid id, CancellationToken ct)
    {
        var success = await _elasticAdmin.RemoveAsync(id, ct);

        return success;
    }

    public async Task<long?> RemoveAllAsync(CancellationToken ct)
    {
        var removedItems = await _elasticAdmin.RemoveAllAsync(ct);

        return removedItems;
    }
    
    private PlanResponse Map(PlanSearchResult planDocument) => new PlanResponse(
        planDocument.Id,
        planDocument.Tags,
        planDocument.Description,
        planDocument.Exercises
            .Select(pee => new ExerciseResponse(
                pee.Id, 
                pee.Name,
                pee.MuscleGroup, 
                pee.Description,
                pee.Sets,
                pee.Reps, 
                pee.Notes))
            .ToList());

    private PlanEntity Map(PlanIndexRequest planRequest) => new PlanEntity
    {
        Id = planRequest.Id,
        Description = planRequest.Description,
        CreatedAt = planRequest.CreatedAt,
        PlanExercises = planRequest.PlanExercises
            .Select(pe => new PlanExerciseEntity
            {
                PlanId = planRequest.Id,
                Plan = null,
                ExerciseId = pe.ExerciseId,
                Exercise = pe.Exercise,
                Order = pe.Order,
                Sets = pe.Sets,
                Reps = pe.Reps
            }).ToList()
    };
    
    private CustomPlanEntity Map(CustomPlanIndexRequest request) => new CustomPlanEntity
    {
        Id = request.Id,
        Description = request.Description,
        CreatedAt = request.CreatedAt,
        UserId = request.UserId,
        CompletionDate = request.CompletionDate.HasValue 
            ? DateOnly.FromDateTime(request.CompletionDate.Value)
            : null,
        PlanExercises = request.PlanExercises
            .Select(cpe => new CustomPlanExerciseEntity
            {
                PlanId = request.Id,
                CustomPlan = null,
                ExerciseId = cpe.ExerciseId,
                Exercise = cpe.Exercise,
                Order = cpe.Order,
                Sets = cpe.Sets,
                Reps = cpe.Reps,
                Notes = cpe.Notes
            })
            .ToList()
    };
}