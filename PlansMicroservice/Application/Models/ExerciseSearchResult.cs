namespace TrainingPlans.Application.Models;

public class ExerciseSearchResult
{
    public Guid Id { get; init; }
    public string Name { get; init; }
    public string MuscleGroup { get; init; }
    public string? Description { get; init; }
    public int? Sets { get; init; }
    public int? Reps { get; init; }
    public string? Notes { get; init; }
}