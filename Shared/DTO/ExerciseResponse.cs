
namespace Shared.DTO;

public record ExerciseResponse(
    Guid Id,
    string Name,
    string MuscleGroup,
    string? Description,
    int? Sets,
    int? Reps,
    string? Notes);
