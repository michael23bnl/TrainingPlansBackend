using TrainingPlans.Domain.Entities;

namespace TrainingPlans.API.DTO;

public record CustomPlanExerciseIndexRequest(
    Guid ExerciseId,
    ExerciseEntity Exercise,
    int Order,
    int? Sets,
    int? Reps,
    string? Notes
);