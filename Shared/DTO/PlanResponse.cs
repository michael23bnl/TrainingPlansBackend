
namespace Shared.DTO;

public record PlanResponse(
    Guid Id,
    List<string> Tags,
    List<ExerciseResponse> Exercises);