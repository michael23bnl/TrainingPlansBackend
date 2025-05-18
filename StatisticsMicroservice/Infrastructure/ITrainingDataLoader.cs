using StatisticsMicroservice.Models;

namespace StatisticsMicroservice.Services.Interfaces;

public interface ITrainingDataLoader
{
    public List<TrainingData> LoadFromJsonFile(string filePath);
}