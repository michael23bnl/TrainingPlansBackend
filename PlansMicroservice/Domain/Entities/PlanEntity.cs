
namespace TrainingPlans.Domain.Entities;

public class PlanEntity
{
    public Guid Id { get; set; }
    public string? Description { get; set; }
    public DateTime CreatedAt { get; set; }
    public List<PlanExerciseEntity> PlanExercises { get; set; } = [];
}