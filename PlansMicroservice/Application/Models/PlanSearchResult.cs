namespace TrainingPlans.Application.Models;

public class PlanSearchResult
{
    public Guid Id { get; init; }
    public string? Description { get; init; }
    public List<string> Tags { get; init; } = [];
    public List<ExerciseSearchResult> Exercises { get; init; } = [];
}