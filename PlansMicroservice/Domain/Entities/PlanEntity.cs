
namespace TrainingPlans.Domain.Entities;

public class PlanEntity
{
    public Guid Id { get; set; }

    public List<string> Tags => Exercises
        .Select(e => e.MuscleGroup)
        .Distinct()
        .ToList();
    
    public List<ExerciseEntity> Exercises { get; set; } = [];
    
    public Guid? CreatedBy { get; set; } // если created by = null, значит план загружен в систему заранее
}