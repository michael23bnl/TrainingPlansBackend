using TrainingPlans.API.DTO;
using TrainingPlans.Pagination;

namespace TrainingPlans.Application.Services.Interfaces;

public interface IFavoritePlansService
{
    Task AddPlanToFavorites(Guid userId, Guid planId);
    Task RemovePlanFromFavorites(Guid userId, Guid planId);
    Task<(int, List<CompletedPlanResponse>)> GetFavoritePlans(Guid userId, PlanParameters planParameters);
    Task EditFavoritePlan(Guid planId, PlanRequest request, Guid userId);
}