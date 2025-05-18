using StatisticsMicroservice.Models;

namespace StatisticsMicroservice.Repositories;

public interface IStatisticsRepository
{
    public Task<List<Statistic>> Get(Guid userId);

    public Task SaveStatistics(List<Statistic> statistics);
    
    public Task RemoveStatistics(Guid userId, Guid planId);
}