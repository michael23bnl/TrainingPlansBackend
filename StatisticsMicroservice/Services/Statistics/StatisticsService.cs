using System.Collections.Concurrent;
using System.Globalization;
using System.Text.Json;
using StatisticsMicroservice.Models;
using StatisticsMicroservice.Repositories;
using StatisticsMicroservice.Services.Interfaces;

namespace StatisticsMicroservice.Services.Statistics;

public class StatisticsService : IStatisticsService
{
    private readonly IStatisticsRepository _repository;
    private readonly IExerciseCategoryIdentifier _identifier;

    public StatisticsService(IStatisticsRepository repository, 
        IExerciseCategoryIdentifier identifier)
    {
        _repository = repository;
        _identifier = identifier;
    }

    public async Task<List<Statistic>> GetStatistics(Guid userId)
    {
        var statistics = await _repository.Get(userId);
        
        return statistics;
    }
    
    public async Task SetStatistics(string jsonStatisticItems)
    {
        var data = JsonSerializer.Deserialize<ExercisesData>(jsonStatisticItems);

        var statistics = new List<Statistic>();
        
        foreach (var exercise in data.Exercises)
        {
            var statistic = new Statistic
            {
                Id = Guid.NewGuid(),
                UserId = Guid.Parse(data.UserId),
                MuscleGroup = 
                    !string.IsNullOrEmpty(exercise.Category) 
                    ? exercise.Category 
                    : _identifier.PredictCategory(exercise.Name)
            };
            statistics.Add(statistic);
        };
        
        await _repository.SaveStatistics(statistics);
    }
}