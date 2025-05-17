namespace StatisticsMicroservice.Models;

public class Statistic
{
    public Guid Id { get; set; }
    
    public Guid UserId { get; set; }
    
    public string MuscleGroup { get; set; }
    
    public DateOnly CompletionDate { get; set; } = DateOnly.FromDateTime(DateTime.Now);
}