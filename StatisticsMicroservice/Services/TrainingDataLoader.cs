using StatisticsMicroservice.Models;
using StatisticsMicroservice.Services.Interfaces;

namespace StatisticsMicroservice;

using System.Text.Json;
using System.Text.Json.Serialization;

public class TrainingDataLoader : ITrainingDataLoader
{
    public List<TrainingData> LoadFromJsonFile(string filePath)
    {
        var json = File.ReadAllText(filePath);
        var items = JsonSerializer.Deserialize<List<TrainingJsonItem>>(json);

        return items.Select(item => new TrainingData
        {
            ExercisesDescription = string.Join(", ", item.Exercises),
            Category = item.Category
        }).ToList();
    }
}
