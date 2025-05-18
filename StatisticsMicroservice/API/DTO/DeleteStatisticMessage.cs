namespace StatisticsMicroservice.Models;

public class DeleteStatisticMessage
{
    public Guid UserId { get; set; }
    public Guid PlanId { get; set; }
}