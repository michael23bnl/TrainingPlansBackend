using System.Collections.Concurrent;
using TrainingPlans.Models;

namespace TrainingPlans.Services.Statistics;

public interface IStatisticsService
{
    public ConcurrentDictionary<string, int> GetStatistics(List<PlanModel> plans);
}