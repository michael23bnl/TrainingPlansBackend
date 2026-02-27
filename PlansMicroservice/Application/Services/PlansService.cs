
using Shared.DTO;
using TrainingPlans.Domain.Abstractions;
using TrainingPlans.Domain.Entities;
using Shared.Pagination;
using TrainingPlans.Domain.DTO;

namespace TrainingPlans.Application.Services;

public class PlansService : IPlansService
{
    private readonly IPlansRepository _plansRepository;

    public PlansService(IPlansRepository plansRepository)
    {
        _plansRepository = plansRepository;
    }
    
    public async Task<Guid> CreatePlanAsync(List<PlanExercise> exercises, string? description, CancellationToken ct)
    {
        var planId = await _plansRepository.CreateAsync(exercises, description, ct);
        
        return planId;
    }

    public async Task<(int, List<PlanResponse>)> GetAllPlansAsync(PlanParameters? planParameters,
        CancellationToken ct)
    {
        var plans = await _plansRepository.GetAllAsync(planParameters, ct);
        var planResponse = plans.Item2
            .Select(p => Map(p))
            .ToList();

        return (plans.Item1, planResponse);
    }
    
    public async Task<List<PlanEntity>> GetAllPlansAsync(CancellationToken ct)
    {
        var plans = await _plansRepository.GetAllAsync(null, ct);

        return plans.Item2;
    }

    public async Task<PlanResponse?> GetPlanAsync(Guid planId, CancellationToken ct)
    {
        var plan = await _plansRepository.GetAsync(planId, ct);
        var planResponse = Map(plan);
        
        return planResponse;
    }

    public async Task<List<PlanResponse>> GetPlansByIdsAsync(List<Guid> planIds, CancellationToken ct)
    {
        var plans = await _plansRepository.GetByIdsAsync(planIds, ct);
        var planResponse = plans
            .Select(p => Map(p))
            .ToList();
    
        return planResponse;
    }

    public async Task<Guid> UpdatePlanAsync(Guid id, List<PlanExercise>? exercises, 
        string? description, CancellationToken ct)
    {
        var planId = await _plansRepository.UpdateAsync(id, exercises, description, ct);
        
        return planId;
    }
    
    public async Task<Guid> DeletePlanAsync(Guid id, CancellationToken ct)
    {
        var planId = await _plansRepository.DeleteAsync(id, ct);

        return planId;
    }

    private PlanResponse Map(PlanEntity plan) => new PlanResponse(
        plan.Id,
        plan.PlanExercises
            .Select(pe => pe.Exercise.MuscleGroup)
            .Distinct()
            .ToList(),
        plan.Description,
        plan.PlanExercises
            .Select(pe => new ExerciseResponse(
                pe.ExerciseId,
                pe.Exercise.Name,
                pe.Exercise.MuscleGroup,
                pe.Exercise.Description,
                pe.Sets,
                pe.Reps,
                null))
            .ToList());
}