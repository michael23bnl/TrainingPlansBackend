
namespace TrainingPlans.Contracts;

public record PreparedPlanResponse(
    Guid Id,
    string? Category,
    List<ExerciseResponse> Exercises,
    bool? IsFavorite);