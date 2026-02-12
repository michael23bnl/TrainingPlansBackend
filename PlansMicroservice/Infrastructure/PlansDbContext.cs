
using Microsoft.EntityFrameworkCore;
using TrainingPlans.Domain.Entities;
using TrainingPlans.Infrastructure.Configurations;

namespace TrainingPlans.Infrastructure;

public class PlansDbContext(DbContextOptions<PlansDbContext> options) : DbContext(options)
{
    
    public DbSet<ExerciseEntity> Exercises { get; set; }
    
    public DbSet<PlanEntity> Plans { get; set; }
    
    public DbSet<CustomPlanEntity> CustomPlans { get; set; }
    
    public DbSet<PlanExerciseEntity> PlanExercises { get; set; }
    
    public DbSet<CustomPlanExerciseEntity> CustomPlanExercises { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfiguration(new PlanConfiguration());
        modelBuilder.ApplyConfiguration(new ExerciseConfiguration());
        modelBuilder.ApplyConfiguration(new PlanExerciseConfiguration());
        modelBuilder.ApplyConfiguration(new CustomPlanConfiguration());
        modelBuilder.ApplyConfiguration(new PlanExerciseConfiguration());
        modelBuilder.ApplyConfiguration(new CustomPlanExerciseConfiguration());
    }
    
}
