
namespace TrainingPlans.Domain.Entities;

public class CustomPlanExerciseEntity
{
    public Guid PlanId { get; set; }
    public CustomPlanEntity CustomPlan { get; set; }
    public Guid ExerciseId { get; set; }
    public ExerciseEntity Exercise { get; set; }
    public int Order { get; set; }
    public int? Sets { get; set; }
    public int? Reps { get; set; }
    public string? Notes { get; set; }
}