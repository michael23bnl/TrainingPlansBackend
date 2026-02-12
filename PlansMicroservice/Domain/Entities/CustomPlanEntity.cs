

namespace TrainingPlans.Domain.Entities;

public class CustomPlanEntity
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public Guid? SourcePlanId { get; set; }
    public PlanEntity? SourcePlan { get; set; }
    public string? Description { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateOnly? CompletionDate { get; set; }
    public List<CustomPlanExerciseEntity> PlanExercises { get; set; } = [];
}