namespace TrainingPlans.Domain.Entities;

public class PlanSearchResultEntity
{
    public Guid Id { get; set; }
    public string? Category { get; set; }
    public string Exercises { get; set; } = null!;
    public Guid? CreatedBy { get; set; }
    public float Rank { get; set; }
}