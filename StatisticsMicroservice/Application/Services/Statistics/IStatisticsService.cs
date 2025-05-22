using StatisticsMicroservice.Models;

namespace StatisticsMicroservice.Services.Interfaces;

public interface IStatisticsService
{
    public Task<List<Statistic>> GetStatistics(Guid userId, string period);
    public Task SetStatistics(string jsonItems);
    public Task DeleteStatistics(string jsonItems);
}