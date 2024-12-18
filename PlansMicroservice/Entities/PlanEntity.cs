namespace TrainingPlans.Entities;

public class PlanEntity
{
    public Guid Id { get; set; }
    
    public List<ExerciseEntity> Exercises {get; set;} 
}