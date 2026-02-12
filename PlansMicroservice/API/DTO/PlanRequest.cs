
using TrainingPlans.Domain.DTO;

namespace TrainingPlans.API.DTO;

public record PlanRequest(List<PlanExercise> Exercises, string? Description);