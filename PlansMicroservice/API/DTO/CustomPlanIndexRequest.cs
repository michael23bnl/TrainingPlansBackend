namespace TrainingPlans.API.DTO;

public record CustomPlanIndexRequest(
    Guid Id,
    string? Description,
    DateTime CreatedAt,
    Guid UserId,
    DateTime? CompletionDate,
    List<CustomPlanExerciseIndexRequest> PlanExercises
);