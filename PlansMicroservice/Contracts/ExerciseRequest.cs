namespace TrainingPlans.Contracts;

public record ExerciseRequest(
    string name,
    string? muscleGroup);