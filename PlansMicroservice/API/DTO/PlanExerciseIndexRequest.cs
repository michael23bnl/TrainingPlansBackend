using TrainingPlans.Domain.Entities;

namespace TrainingPlans.API.DTO;

public record PlanExerciseIndexRequest(
    Guid ExerciseId, 
    ExerciseEntity Exercise, 
    int? Sets, 
    int? Reps, 
    int Order);