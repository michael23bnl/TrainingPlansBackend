

namespace TrainingPlans.API.DTO;
public record CompletedPlanResponse(
    Guid Id,
    string? Category,
    List<ExerciseResponse> Exercises,
    bool? IsCompleted);