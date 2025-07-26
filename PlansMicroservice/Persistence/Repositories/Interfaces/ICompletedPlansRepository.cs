using TrainingPlans.Domain.Models;
using TrainingPlans.Pagination;

namespace TrainingPlans.Persistence.Repositories.Interfaces;

public interface ICompletedPlansRepository
{
    public Task MarkAsCompleted(Guid userid, Guid planId);

    public Task RemoveCompletedMark(Guid userid, Guid planId);

    public Task<CompletedPlanModel> GetCompletedPlan(Guid userId, Guid planId);

    public Task<List<CompletedPlanModel>> GetCompletedPlans(Guid userId);
    
    public Task<(int, List<PlanModel?>)> GetCompletedPlansPaginated(Guid userId, PlanParameters planParameters);
}