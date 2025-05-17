using Microsoft.EntityFrameworkCore;
using StatisticsMicroservice.Models;

namespace StatisticsMicroservice.Repositories;

public class StatisticsRepository : IStatisticsRepository
{
    
    private readonly StatisticsDbContext _context;

    public StatisticsRepository(StatisticsDbContext context)
    {
        _context = context;
    }
    
    public async Task<List<Statistic>> Get(Guid userId)
    {
        var statistics = await _context.Statistics
            .Where(s => s.UserId == userId)
            .ToListAsync();

        return statistics;
    }

    public async Task SaveStatistics(List<Statistic> statistics)
    {
        await _context.Statistics.AddRangeAsync(statistics);
        await _context.SaveChangesAsync();
    }
}