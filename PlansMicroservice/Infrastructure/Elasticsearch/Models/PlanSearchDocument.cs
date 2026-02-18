namespace TrainingPlans.Infrastructure.Elasticsearch.Models;

public class PlanSearchDocument
{
    public Guid Id { get; set; }
    public string? Description { get; set; }
    public DateTime CreatedAt { get; set; }
    public List<string> Tags { get; set; } = [];
    public List<ExerciseSearchDocument> Exercises { get; set; } = [];
}