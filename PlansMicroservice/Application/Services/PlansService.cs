
using Shared.DTO;
using TrainingPlans.Domain.Abstractions;
using TrainingPlans.Domain.Entities;
using Shared.Pagination;
using TrainingPlans.Application.Abstractions;


namespace TrainingPlans.Application.Services;

public class PlansService : IPlansService
{
    
    private readonly IPlansRepository _plansRepository;
    //private readonly IPlansSearch _plansSearch; 


    public PlansService(IPlansRepository plansRepository)
    {
        _plansRepository = plansRepository;
        //_plansSearch = plansSearch;
    }
    
    public async Task<Guid> CreatePlanAsync(List<Guid> exerciseIds, Guid? userId, CancellationToken ct)
    {
        var planId = await _plansRepository.CreateAsync(exerciseIds, userId, ct);
        
        return planId;
        
        /*await using var transaction = await _plansDbContext.Database.BeginTransactionAsync();
        
        try
        {
            var planId = await _plansRepository.CreateAsync(exercises, userId);
            var planSearchResponse = await _plansSearch.AddOrUpdateAsync(plan);
            if (!planSearchResponse)
            {
                await transaction.RollbackAsync();
                return Guid.Empty;
            }
            await transaction.CommitAsync();
            return planId;
        }
        catch(Exception ex)
        {
            await transaction.RollbackAsync();
            return Guid.Empty;
        }*/
    }

    public async Task<(int, List<PlanResponse>)> GetAllPreloadedPlansAsync(PlanParameters planParameters, CancellationToken ct)
    {
        var plans = await _plansRepository.GetAllPreloadedAsync(planParameters, ct);
        
        var planResponse = plans.Item2
            .Select(pe => new PlanResponse 
            ( 
                pe.Id, 
                pe.Tags, 
                pe.Exercises
                    .Select(e => new ExerciseResponse(e.Id, e.Name, e.MuscleGroup))
                    .ToList()
            ))
            .ToList();

        return (plans.Item1, planResponse);
    }
    
    public async Task<List<PlanEntity>> GetAllPreloadedPlansAsync(CancellationToken ct)
    {
        var plans = await _plansRepository.GetAllPreloadedAsync(ct);

        return plans;
    }

    public async Task<PlanResponse?> GetPlanAsync(Guid planId, Guid userId, CancellationToken ct)
    {
        var plan = await _plansRepository.GetAsync(planId, userId, ct);
        var exerciseResponse = plan.Exercises
            .Select(e => new ExerciseResponse(e.Id, e.Name, e.MuscleGroup)).ToList();
        var planResponse = new PlanResponse(plan.Id, plan.Tags, exerciseResponse);
        
        return planResponse;
    }

    public async Task<List<PlanEntity>> GetPlansAsync(List<Guid> planIds, CancellationToken ct)
    {
        var plans = await _plansRepository.GetAsync(planIds, ct);

        return plans;
    }

    public async Task<Guid> UpdatePlanAsync(Guid id, List<Guid> exerciseIds, CancellationToken ct)
    {
        var planId = await _plansRepository.UpdateAsync(id, exerciseIds, ct);
        
        return planId;
        /*await using var transaction = await _plansDbContext.Database.BeginTransactionAsync();

        try
        {
            var exercises = request.Exercises.Select(e => ExerciseModel.Create(
                Guid.NewGuid(),
                e.Name,
                e.MuscleGroup
            ).exerciseModel).ToList();

            var updatedPlanId = await _plansRepository.Update(planId, request.Category, exercises);

            var planModel = PlanModel.Create(
                updatedPlanId,
                request.Category,
                exercises.Select(p => ExerciseModel.Create(
                    p.Id,
                    p.Name,
                    p.MuscleGroup
                ).exerciseModel).ToList()!,
                userId
            ).planModel!;

            var planSearchResponse = await _plansSearch.AddOrUpdateAsync(planModel);

            if (!planSearchResponse)
            {
                await transaction.RollbackAsync();
                return Guid.Empty;
            }
            await transaction.CommitAsync();
            return updatedPlanId;
        }
        catch(Exception ex)
        {
            await transaction.RollbackAsync();
            return Guid.Empty;
        }*/
    }
    
    public async Task<Guid> DeletePlanAsync(Guid id, CancellationToken ct)
    {
        var planId = await _plansRepository.DeleteAsync(id, ct);

        return planId;
        /*await using var transaction = await _plansDbContext.Database.BeginTransactionAsync();

        try
        {
            var deletedPlanId = await _plansRepository.Delete(id);
            var planSearchResponse = await _plansSearch.RemoveAsync(id.ToString());
            if (!planSearchResponse)
            {
                await transaction.RollbackAsync();
                return Guid.Empty;
            }
            await transaction.CommitAsync();
            return deletedPlanId;
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            return Guid.Empty;
        }*/
    }
    
    /*public async Task<(int totalCount, List<PlanModel> plans)> SearchCatalogPlans(string query, PlanParameters planParameters)
    {
        var plans = await _plansRepository.Search(query, planParameters, (Guid?)null);
        return (plans.plans.Count, plans.plans);
    }
    
    public async Task<(int totalCount, List<PlanModel> plans)> SearchFavoritePlans(string query, PlanParameters planParameters, Guid userId)
    {
        var favoritePlansIds = await GetFavoritePlansIds(userId);
        var plans = await _plansRepository.Search(query, planParameters, favoritePlansIds);
        return (plans.plans.Count, plans.plans);
    }
    
    public async Task<(int totalCount, List<PlanModel> plans)> SearchMyPlans(string query, PlanParameters planParameters, Guid userId)
    {
        var plans = await _plansRepository.Search(query, planParameters, userId);
        return (plans.plans.Count, plans.plans);
    }
    
    public async Task<(int totalCount, List<PlanModel> plans)> SearchCompletedPlans(string query, PlanParameters planParameters, Guid userId)
    {
        var completedPlansIds = await GetCompletedPlansIds(userId);
        var plans = await _plansRepository.Search(query, planParameters, completedPlansIds);
        return (plans.plans.Count, plans.plans);
    }*/
}