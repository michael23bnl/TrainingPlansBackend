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
        var data = JsonSerializer.Deserialize<CreateStatisticMessage>(jsonStatisticItems);

        var statistics = new List<Statistic>();
        
        foreach (var exercise in data.Exercises)
        {
            var statistic = new Statistic
            {
                UserId = data.UserId,
                PlanId = data.PlanId,
                MuscleGroup = 
                    !string.IsNullOrEmpty(exercise.MuscleGroup) 
                    ? exercise.MuscleGroup 
                    : _identifier.PredictCategory(exercise.Name),
                CompletionDate = data.CompletionDate,
            };
            statistics.Add(statistic);
        };
        
        await _repository.SaveStatistics(statistics);
    }
    
    public async Task DeleteStatistics(string jsonIds)
    {
        var data = JsonSerializer.Deserialize<DeleteStatisticMessage>(jsonIds);

        await _repository.RemoveStatistics(data.UserId, data.PlanId);
    }
}