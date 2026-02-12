

namespace TrainingPlans.Domain.Entities;

public class ExerciseEntity
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string MuscleGroup { get; set; }
    public string Description { get; set; }
}