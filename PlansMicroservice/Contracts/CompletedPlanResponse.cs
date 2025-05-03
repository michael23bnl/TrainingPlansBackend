namespace TrainingPlans.Contracts;

public record CompletedPlanResponse(
    Guid Id,
    string? Category,
    List<ExerciseResponse> Exercises,
    bool? IsCompleted);