
using TrainingPlans.Entities;
using TrainingPlans.Models;

namespace TrainingPlans.Repositories.Interfaces;

public interface IFavoritePlansRepository
{
    public Task AddToFavorites(Guid userid, Guid planId);

    public Task RemoveFromFavorites(Guid userid, Guid planId);

    public Task<List<PlanModel>> GetFavorites(Guid userId);

    public Task EditFavorite(Guid userId, Guid planId, string? name, List<ExerciseModel> exercises);
}