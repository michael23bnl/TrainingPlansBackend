

namespace TrainingPlans.API.DTO;
public record ExerciseResponse(
    Guid Id,
    string Name,
    string? MuscleGroup);
