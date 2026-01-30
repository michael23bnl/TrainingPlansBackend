

namespace TrainingPlans.Domain.Entities;

public class CustomPlanEntity
{
    
    public Guid UserId { get; set; }

    public Guid PlanId { get; set; }

    public DateOnly? CompletionDate { get; set; }
}