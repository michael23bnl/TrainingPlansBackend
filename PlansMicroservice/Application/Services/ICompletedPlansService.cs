using TrainingPlans.Models;
using TrainingPlans.Pagination;

namespace TrainingPlans.Application.Services;

public interface ICompletedPlansService
{
    Task MarkAsCompleted(Guid userId, Guid planId);
    Task<(int, List<PlanModel>)> GetCompletedPlans(Guid userId, PlanParameters planParameters);
    Task RemoveCompletedMark(Guid userId, Guid planId);
    Task<List<CompletedPlanModel>> GetCompletedPlans(Guid userId);
}