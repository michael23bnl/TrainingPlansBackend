namespace TrainingPlans.API.DTO;

public record PlanIndexRequest(
    Guid Id, 
    string? Description, 
    DateTime CreatedAt, 
    List<PlanExerciseIndexRequest> PlanExercises);