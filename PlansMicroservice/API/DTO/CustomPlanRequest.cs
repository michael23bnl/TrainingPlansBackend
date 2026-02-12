using TrainingPlans.Domain.DTO;

namespace TrainingPlans.API.DTO;

public record CustomPlanRequest(List<CustomPlanExercise> Exercises, Guid? SourcePlanId, string? Description);