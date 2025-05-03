using TrainingPlans.Models;

namespace TrainingPlans.Repositories.Interfaces;

public interface ICompletedPlansRepository
{
    public Task MarkAsCompleted(Guid userid, Guid planId);

    public Task RemoveCompletedMark(Guid userid, Guid planId);

    public Task<List<PlanModel>> GetCompletedPlans(Guid userId);
}