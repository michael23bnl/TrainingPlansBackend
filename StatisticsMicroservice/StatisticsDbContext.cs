using Microsoft.EntityFrameworkCore;
using StatisticsMicroservice.Models;

namespace StatisticsMicroservice;

public class StatisticsDbContext : DbContext
{

    public StatisticsDbContext(DbContextOptions<StatisticsDbContext> options) : base(options)
    {
        
    }
    
    public DbSet<Statistic> Statistics { get; set; }
    
}