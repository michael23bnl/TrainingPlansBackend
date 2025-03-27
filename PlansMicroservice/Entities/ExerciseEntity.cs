

namespace TrainingPlans.Entities;

public class ExerciseEntity
{
    public Guid Id { get; set; }
    
    public string Name { get; set; }
    
    public string? MuscleGroup { get; set; }
    public Guid? CreatedBy { get; set; } // если created by = null, значит план загружен в систему заранее
    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
}