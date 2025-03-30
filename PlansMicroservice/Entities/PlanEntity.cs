

namespace TrainingPlans.Entities;

public class PlanEntity
{
    public Guid Id { get; set; }
    
    public string? Category { get; set; }
    
    public List<ExerciseEntity> Exercises { get; set; } = new List<ExerciseEntity>();
    
    public Guid? CreatedBy { get; set; } // если created by = null, значит план загружен в систему заранее
}