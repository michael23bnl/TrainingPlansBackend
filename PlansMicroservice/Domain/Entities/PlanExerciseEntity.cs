namespace TrainingPlans.Domain.Entities;

public class PlanExerciseEntity
{
    public Guid PlanId { get; set; }
    public PlanEntity Plan { get; set; }
    public Guid ExerciseId { get; set; }
    public ExerciseEntity Exercise { get; set; }
    public int Order { get; set; }
    public int? Sets { get; set; }
    public int? Reps { get; set; }
}