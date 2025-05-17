using TrainingPlans.Models;

namespace TrainingPlans.Contracts;

public record PlanRequest(
    string? Category, 
    List<ExerciseRequest>? Exercises
    );