using StatisticsMicroservice.Models;

namespace StatisticsMicroservice.Services.Interfaces;

public interface IStatisticsService
{
    public Task<List<Statistic>> GetStatistics(Guid userId);
    public Task SetStatistics(string jsonItems);
}