using Microsoft.EntityFrameworkCore;

using TrainingPlans.Entities;

namespace TrainingPlans;

public class PlansDbContext(DbContextOptions<PlansDbContext> options) : DbContext(options)
{
    
    public DbSet<ExerciseEntity> Exercises { get; set; }
    
    public DbSet<PlanEntity> Plans { get; set; }
    
    public DbSet<FavoritePlanEntity> FavoritePlans { get; set; }
    
}
