namespace TrainingPlans.Contracts;

public record ExerciseRequest(
    string Name,
    string? MuscleGroup);