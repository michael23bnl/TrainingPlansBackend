

namespace TrainingPlans.Contracts;

public record PreparedPlanResponse(
    Guid id,
    string? name,
    List<ExerciseResponse> exercises,
    bool? isFavorite);