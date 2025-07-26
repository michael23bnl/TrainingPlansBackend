using System.Collections.Concurrent;
using System.Globalization;
using System.Text.Json;
using System.Text.RegularExpressions;
using StatisticsMicroservice.Application.Services.Interfaces;
using StatisticsMicroservice.Infrastructure.ML;
using StatisticsMicroservice.Models;
using StatisticsMicroservice.Repositories;
using StatisticsMicroservice.Services.Interfaces;

namespace StatisticsMicroservice.Application.Services;

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

    private bool ValidateExercise(Exercise exercise)
    {
        if (string.IsNullOrEmpty(exercise.Name))
        {
            return false;
        }

        if (!Regex.IsMatch(exercise.Name, @"[а-яА-Я]"))
        {
            return false;
        }

        if (exercise.Name.Length < 3)
        {
            return false;
        }
        
        return true;
    }

    public async Task<List<Statistic>> GetStatistics(Guid userId, string period)
    {
        var isDaysAmount = int.TryParse(period, out var pastDays);
        DateOnly? from = null;
        
        if (isDaysAmount)
        {
            from = DateOnly.FromDateTime(DateTime.UtcNow.AddDays(-pastDays));
        }

        var statistics = await _repository.Get(userId, from);
        
        return statistics;
    }

    public async Task SetStatistics(string jsonStatisticItems)
    {
        var data = JsonSerializer.Deserialize<CreateStatisticMessage>(jsonStatisticItems);

        var statistics = new List<Statistic>();

        foreach (var exercise in data.Exercises)
        {
            if (!ValidateExercise(exercise))
            {
                continue;
            }

            var muscleGroups = new List<string> { "" };

            if (!string.IsNullOrEmpty(exercise.MuscleGroup))
            {
                muscleGroups = exercise.MuscleGroup
                    .Split(',', StringSplitOptions.RemoveEmptyEntries)
                    .Select(c => c.Trim())
                    .ToList();
            }

            foreach (var muscleGroup in muscleGroups)
            {
                var statistic = new Statistic
                {
                    UserId = data.UserId,
                    PlanId = data.PlanId,
                    MuscleGroup =
                        !string.IsNullOrEmpty(muscleGroup)
                            ? muscleGroup
                            : _identifier.DefineCategory(exercise.Name),
                    CompletionDate = data.CompletionDate,
                };
                statistics.Add(statistic);
            }
        }

        await _repository.SaveStatistics(statistics);
    }

    public async Task DeleteStatistics(string jsonIds)
    {
        var data = JsonSerializer.Deserialize<DeleteStatisticMessage>(jsonIds);

        await _repository.RemoveStatistics(data.UserId, data.PlanId);
    }
}