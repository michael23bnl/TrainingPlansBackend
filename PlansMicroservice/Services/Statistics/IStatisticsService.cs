using System.Collections.Concurrent;
using TrainingPlans.Models;

namespace TrainingPlans.Services.Statistics;

public interface IStatisticsService
{
    public ConcurrentDictionary<DateOnly, Dictionary<string, int>> GetStatistics(List<CompletedPlanModel> plans);
}