namespace TrainingPlans.Entities;

public class FavoritePlanEntity
{
    public Guid Id { get; set; }
    
    public Guid UserId { get; set; }

    public Guid PlanId { get; set; }
    
}