
namespace TrainingPlans.Contracts;

public record PreparedPlanResponse(
    Guid Id,
    string? Name,
    List<ExerciseResponse> Exercises,
    bool? IsFavorite);