using TrainingPlans.API.DTO;
using TrainingPlans.Application.Services.Interfaces;
using TrainingPlans.Domain.Models;
using TrainingPlans.Pagination;
using TrainingPlans.Persistence.Repositories.Interfaces;

namespace TrainingPlans.Application.Services;

public class FavoritePlansService : IFavoritePlansService
{
    
    private readonly IFavoritePlansRepository _favoritePlansRepository;
    private readonly IPlansSearch _plansSearch;

    public FavoritePlansService(IFavoritePlansRepository favoritePlansRepository,
        IPlansSearch plansSearch)
    {
        _favoritePlansRepository = favoritePlansRepository;
        _plansSearch = plansSearch;
    }
    
    public async Task AddPlanToFavorites(Guid userId, Guid planId)
    {
        await _favoritePlansRepository.AddToFavorites(userId, planId);
    }
    
    public async Task RemovePlanFromFavorites(Guid userId, Guid planId)
    {
        await _favoritePlansRepository.RemoveFromFavorites(userId, planId);
    }
    
    public async Task<(int, List<CompletedPlanResponse>)> GetFavoritePlans(Guid userId, PlanParameters planParameters)
    {
        var response = await _favoritePlansRepository.GetFavorites(userId, planParameters);

        return response;
    }
    
    public async Task EditFavoritePlan(Guid planId, PlanRequest request, Guid userId)
    {
        var exercises = request.Exercises.Select(e => ExerciseModel.Create(
            Guid.NewGuid(), 
            e.Name,
            e.MuscleGroup
        ).exerciseModel).ToList();

        var updatedPlanId = await _favoritePlansRepository.EditFavorite(userId, planId, request.Category, exercises);
        
        var plan = await _favoritePlansRepository.GetFavorite(updatedPlanId);

        var planModel = PlanModel.Create(
            plan.Id, 
            plan.Category, 
            plan.Exercises.Select(e => ExerciseModel.Create(
                e.Id,
                e.Name,
                e.MuscleGroup
            ).exerciseModel).ToList(), 
            plan.CreatedBy
        ).planModel;
        
        var result = await _plansSearch.AddOrUpdateAsync(planModel);
    }
}