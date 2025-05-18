namespace StatisticsMicroservice.Models;

public class Statistic
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid UserId { get; set; }
    public Guid PlanId { get; set; }
    public string MuscleGroup { get; set; }
    public DateOnly CompletionDate { get; set; }
}