namespace TrainingPlans.Entities;

public class CompletedPlanEntity
{
    public Guid Id { get; set; }
    
    public Guid UserId { get; set; }

    public Guid PlanId { get; set; }
    
    public DateTime CompletionDate { get; set; } = DateTime.UtcNow;
}