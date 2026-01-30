
using Microsoft.EntityFrameworkCore;
using TrainingPlans.Persistence.Configurations;
using TrainingPlans.Domain.Entities;

namespace TrainingPlans.Persistence;

public class PlansDbContext(DbContextOptions<PlansDbContext> options) : DbContext(options)
{
    
    public DbSet<ExerciseEntity> Exercises { get; set; }
    
    public DbSet<PlanEntity> Plans { get; set; }
    
    public DbSet<CustomPlanEntity> CustomPlans { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfiguration(new PlanConfiguration());
        modelBuilder.ApplyConfiguration(new ExerciseConfiguration());
        modelBuilder.ApplyConfiguration(new PlanExerciseConfiguration());
        modelBuilder.ApplyConfiguration(new CustomPlanConfiguration());
    }
    
}
