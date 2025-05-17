namespace TrainingPlans.Entities;

public class CompletedPlanEntity
{
    public Guid Id { get; set; }
    
    public Guid UserId { get; set; }

    public Guid PlanId { get; set; }
    
    public DateOnly CompletionDate { get; set; } = DateOnly.FromDateTime(DateTime.Now);
}