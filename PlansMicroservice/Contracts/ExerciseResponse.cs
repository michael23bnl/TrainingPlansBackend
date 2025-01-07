namespace TrainingPlans.Contracts;

public record ExerciseResponse(
    Guid id,
    string name,
    string? muscleGroup);
