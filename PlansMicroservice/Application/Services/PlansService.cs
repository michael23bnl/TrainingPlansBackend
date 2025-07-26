using TrainingPlans.API.DTO;
using TrainingPlans.Application.Services.Interfaces;
using TrainingPlans.Domain.Models;
using TrainingPlans.Pagination;
using TrainingPlans.Persistence.Repositories.Interfaces;

namespace TrainingPlans.Application.Services;

public class PlansService : IPlansService
{
    
    private readonly IPlansRepository _plansRepository;
    private readonly IPlansSearch _plansSearch; 
    private readonly PlansDbContext _plansDbContext;

    public PlansService(IPlansRepository plansRepository, IPlansSearch plansSearch, 
        PlansDbContext plansDbContext)
    {
        _plansRepository = plansRepository;
        _plansSearch = plansSearch;
        _plansDbContext = plansDbContext;
    }
    
    public async Task<Guid> CreatePlan(PlanRequest request, Guid? userId)
    {
        var exercises = request.Exercises.Select(e => ExerciseModel.Create(
            Guid.NewGuid(), 
            e.Name,
            e.MuscleGroup
        ).exerciseModel).ToList();
        
        var (plan, response) = PlanModel.Create(Guid.NewGuid(), request.Category, exercises, userId);
        
        if (response != "Plan has been created")
        {
            return Guid.Empty;
        }
        
        await using var transaction = await _plansDbContext.Database.BeginTransactionAsync();
        
        try
        {
            var planId = await _plansRepository.Create(plan);
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
        }
    }
    
    public async Task<(int, List<CompletedPlanResponse>)> GetAllSelfMadePlans(PlanParameters planParameters, Guid userId)
    {
        var response = await _plansRepository.GetAllSelfMade(userId, planParameters);

        return response;
    }
    
    public async Task<List<PreparedPlanResponse>> GetAllAvailablePlans(Guid userId)
    {
        var plans = await _plansRepository.GetAllAvailable(userId);
        
        return plans;
    }
    
    public async Task<(int, List<PreparedPlanResponse>)> GetAllPreparedPlans(PlanParameters planParameters, Guid? userId)
    {
        var response = await _plansRepository.GetAllPrepared(userId, planParameters);
        
        return response;

    }
    
    public async Task<PreparedPlanResponse> GetPlan(Guid planId, Guid userId)
    {
        var plan = await _plansRepository.Get(planId, userId);
        
        return plan;
    }
    
    public async Task<Guid> UpdatePlan(Guid planId, PlanRequest request, Guid userId)
    {
        
        await using var transaction = await _plansDbContext.Database.BeginTransactionAsync();

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
        }
    }
    
    public async Task<Guid> DeletePlan(Guid id)
    {
        
        await using var transaction = await _plansDbContext.Database.BeginTransactionAsync();

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
        }
    }

    public async Task<List<Guid>> GetFavoritePlansIds(Guid userId)
    {
        var ids = await _plansRepository.GetFavoritePlanIds(userId);
        return ids;
    }
    
    public async Task<List<Guid>> GetCompletedPlansIds(Guid userId)
    {
        var ids = await _plansRepository.GetCompletedPlanIds(userId);
        return ids;
    }

    public async Task<(int totalCount, List<PlanModel> plans)> SearchCatalogPlans(string query, PlanParameters planParameters)
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
    }
}