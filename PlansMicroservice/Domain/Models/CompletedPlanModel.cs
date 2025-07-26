using TrainingPlans.Entities;

namespace TrainingPlans.Domain.Models;

public class CompletedPlanModel
{
    public PlanModel Plan { get; set; } = null!;
    public DateOnly CompletionDate { get; set; }

    public static CompletedPlanModel Create(PlanEntity plan, DateOnly completionDate)
    {
        var planModel = PlanModel.Create(
            plan.Id,
            plan.Category,
            plan.Exercises.Select(e => ExerciseModel.Create(e.Id, e.Name, e.MuscleGroup).exerciseModel).ToList()!,
            plan.CreatedBy
        ).planModel;

        return new CompletedPlanModel
        {
            Plan = planModel,
            CompletionDate = completionDate
        };
    }
}
