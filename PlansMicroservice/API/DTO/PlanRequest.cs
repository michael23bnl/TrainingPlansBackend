

namespace TrainingPlans.API.DTO;

public record PlanRequest(
    string? Category, 
    List<ExerciseRequest>? Exercises
    );