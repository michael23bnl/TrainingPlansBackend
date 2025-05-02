using System.Text.Json;
using Microsoft.EntityFrameworkCore;

using TrainingPlans.Entities;

namespace TrainingPlans;

public class PlansDbContext(DbContextOptions<PlansDbContext> options) : DbContext(options)
{
    
    public DbSet<ExerciseEntity> Exercises { get; set; }
    
    public DbSet<PlanEntity> Plans { get; set; }
    
    public DbSet<FavoritePlanEntity> FavoritePlans { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<PlanEntity>(p =>
        {
            p.Property(x => x.Exercises)
                .HasConversion(
                    v => JsonSerializer.Serialize(v, new JsonSerializerOptions
                    {
                        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                        WriteIndented = false
                    }),
                    v => JsonSerializer.Deserialize<List<ExerciseEntity>>(v, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    }))
                .HasColumnType("jsonb"); // Для PostgreSQL
        });

    }
    
}
