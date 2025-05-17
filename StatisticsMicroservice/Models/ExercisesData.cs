using System.Text.Json.Serialization;

namespace StatisticsMicroservice.Models;

public class ExercisesData
{
    [JsonPropertyName("exercises")]
    public List<Exercise> Exercises { get; set; }
    
    [JsonPropertyName("userId")]
    public string UserId { get; set; }
}