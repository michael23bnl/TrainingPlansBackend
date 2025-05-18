using System.Text.Json.Serialization;

namespace StatisticsMicroservice.Models;

public class CreateStatisticMessage
{
    public Guid UserId { get; set; }
    public Guid PlanId { get; set; }
    public List<Exercise> Exercises { get; set; }
    public DateOnly CompletionDate { get; set; }
    
}