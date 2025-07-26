
using TrainingPlans.API.DTO;
using TrainingPlans.Domain.Models;
using TrainingPlans.Entities;
using TrainingPlans.Pagination;

namespace TrainingPlans.Persistence.Repositories.Interfaces;

public interface IFavoritePlansRepository
{
    public Task AddToFavorites(Guid userid, Guid planId);

    public Task RemoveFromFavorites(Guid userid, Guid planId);

    public Task<(int, List<CompletedPlanResponse>)> GetFavorites(Guid userId, PlanParameters planParameters);
    
    public Task<PlanEntity> GetFavorite(Guid userId);

    public Task<Guid> EditFavorite(Guid userId, Guid planId, string? name, List<ExerciseModel> exercises);
}