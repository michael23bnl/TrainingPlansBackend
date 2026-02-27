
using Shared.DTO;
using TrainingPlans.Domain.Abstractions;
using TrainingPlans.Domain.DTO;
using TrainingPlans.Domain.Entities;

namespace TrainingPlans.Application.Services;

public class CustomPlansService : ICustomPlansService
{
    private readonly ICustomPlansRepository _customPlansRepository;

    public CustomPlansService(ICustomPlansRepository customPlansRepository)
    {
        _customPlansRepository = customPlansRepository;
    }

    public async Task<Guid> CreateCustomPlanAsync(Guid userId, string? description, Guid? sourcePlanId,
        List<CustomPlanExercise> exercises, CancellationToken ct)
    {
        var planId = await _customPlansRepository.CreateAsync(userId, description, sourcePlanId, exercises, ct);

        return planId;
    }

    public async Task<List<PlanResponse>> GetAllCustomPlansAsync(Guid userId, CancellationToken ct)
    {
        var plans =  await _customPlansRepository.GetAllAsync(userId, ct);
        var planResponse = plans
            .Select(cp => Map(cp))
            .ToList();

        return planResponse;
    }
    
    public async Task<PlanResponse?> GetPlanAsync(Guid userId, Guid planId, CancellationToken ct)
    {
        var plan = await _customPlansRepository.GetAsync(userId, planId, ct);
        var planResponse = Map(plan);
        
        return planResponse;
    }

    public async Task<List<PlanResponse>> GetCompletedPlansAsync(Guid userId, CancellationToken ct)
    {
        var plans =  await _customPlansRepository.GetCompletedAsync(userId, ct);
        var planResponse = plans
            .Select(cp => Map(cp))
            .ToList();

        return planResponse;
    }

    public async Task<Guid> UpdateCustomPlanAsync(Guid userId, Guid planId, string? description,
        List<CustomPlanExercise>? exercises, CancellationToken ct)
    {
        var updatedPlanId = await _customPlansRepository.UpdateAsync(userId, planId, description, exercises, ct);
        
        return updatedPlanId;
    }

    public async Task<Guid> DeleteCustomPlanAsync(Guid userId, Guid planId, CancellationToken ct)
    {
        var deletedPlanId = await _customPlansRepository.DeleteAsync(userId, planId, ct);

        return deletedPlanId;
    }

    public async Task<Guid> CompleteCustomPlanAsync(Guid userId, Guid planId, CancellationToken ct)
    {
        var completedPlanId = await _customPlansRepository.CompleteAsync(userId, planId, ct);

        return completedPlanId;
    }

    public async Task<Guid> UncompleteCustomPlanAsync(Guid userId, Guid planId, CancellationToken ct)
    {
        var uncompletedPlanId = await _customPlansRepository.UncompleteAsync(userId, planId, ct);
        
        return uncompletedPlanId;
    }

    private PlanResponse Map(CustomPlanEntity customPlan) => new PlanResponse(
            customPlan.Id,
            customPlan.PlanExercises
                .Select(cpe => cpe.Exercise.MuscleGroup)
                .Distinct()
                .ToList(),
            customPlan.Description,
            customPlan.PlanExercises
                .Select(cpe => new ExerciseResponse(
                    cpe.ExerciseId,
                    cpe.Exercise.Name,
                    cpe.Exercise.MuscleGroup,
                    cpe.Exercise.Description,
                    cpe.Sets,
                    cpe.Reps,
                    cpe.Notes))
                .ToList()
        );
}