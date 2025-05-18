

namespace TrainingPlans.Contracts;

public record ExerciseResponse(
    Guid Id,
    string Name,
    string? MuscleGroup);
