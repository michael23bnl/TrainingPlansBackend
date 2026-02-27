namespace TrainingPlans.Infrastructure.Elasticsearch.Models;

public class ExerciseSearchDocument
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string MuscleGroup { get; set; }
    public string? Description { get; set; }
    public int? Sets { get; set; }
    public int? Reps { get; set; }
    public string? Notes { get; set; }
}