using TrainingPlans.Models;

namespace TrainingPlans.Contracts;

public record PlanRequest(string? name, List<ExerciseRequest>? exercises);