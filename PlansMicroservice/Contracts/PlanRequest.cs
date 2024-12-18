using TrainingPlans.Models;

namespace TrainingPlans.Contracts;

public record PlanRequest(List<ExerciseRequest> exercises);