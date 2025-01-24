

namespace TrainingPlans.Entities;

public class ExerciseEntity
{
    public Guid Id { get; set; }
    
    public string Name { get; set; }
    
    public string? MuscleGroup { get; set; }

    public bool? IsPrepared { get; set; } // при добавлении упражнений в план нет разницы, подготовлены ли они или нет
    
    public Guid? CreatedBy { get; set; }
    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
}