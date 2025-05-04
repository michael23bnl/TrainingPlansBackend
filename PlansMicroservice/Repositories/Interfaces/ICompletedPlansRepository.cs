using TrainingPlans.Models;
using TrainingPlans.Pagination;

namespace TrainingPlans.Repositories.Interfaces;

public interface ICompletedPlansRepository
{
    public Task MarkAsCompleted(Guid userid, Guid planId);

    public Task RemoveCompletedMark(Guid userid, Guid planId);

    public Task<List<PlanModel>> GetCompletedPlans(Guid userId);
    
    public Task<(int, List<PlanModel?>)> GetCompletedPlansPaginated(Guid userId, PlanParameters planParameters);
}