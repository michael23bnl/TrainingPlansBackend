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
    
        var groupedExercises = JsonSerializer.Deserialize<List<TrainingJsonItem>>(json);

        var exerciseToCategories = new Dictionary<string, HashSet<string>>();

        foreach (var group in groupedExercises)
        {
            foreach (var exercise in group.Exercises)
            {
                if (!exerciseToCategories.ContainsKey(exercise))
                {
                    exerciseToCategories[exercise] = new HashSet<string>();
                }

                exerciseToCategories[exercise].Add(group.Category);
            }
        }

        var result = new List<TrainingData>();

        foreach (var kvp in exerciseToCategories)
        {
            result.Add(new TrainingData
            {
                ExercisesDescription = kvp.Key,
                Category = string.Join(", ", kvp.Value)
            });
        }

        return result;
    }
}
