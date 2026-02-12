
namespace Shared.DTO;

public record PlanResponse(
    Guid Id,
    List<string> Tags,
    string? Description,
    List<ExerciseResponse> Exercises);