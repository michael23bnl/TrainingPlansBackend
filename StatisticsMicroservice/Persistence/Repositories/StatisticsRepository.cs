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
    
    public async Task<List<Statistic>> Get(Guid userId, DateOnly? from)
    {
        var query = _context.Statistics.AsQueryable();

        query = query.Where(s => s.UserId == userId);

        if (from.HasValue)
        {
            query = query.Where(s => s.CompletionDate >= from.Value);
        }

        return await query.ToListAsync();
    }


    public async Task SaveStatistics(List<Statistic> statistics)
    {
        await _context.Statistics.AddRangeAsync(statistics);
        await _context.SaveChangesAsync();
    }

    public async Task RemoveStatistics(Guid userId, Guid planId)
    {
        var statistics = await _context.Statistics
            .Where(s => s.UserId == userId && s.PlanId == planId)
            .ToListAsync();
        
        _context.Statistics.RemoveRange(statistics);
        
        await _context.SaveChangesAsync();
    }
}