using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TrainingPlans.Domain.Entities;

namespace TrainingPlans.Infrastructure.Configurations;

public class CustomPlanExerciseConfiguration : IEntityTypeConfiguration<CustomPlanExerciseEntity>
{
    public void Configure(EntityTypeBuilder<CustomPlanExerciseEntity> builder)
    {
        builder.HasKey(pe => new { pe.PlanId, pe.ExerciseId });
        
        builder.HasOne(pe => pe.CustomPlan)
            .WithMany(cp => cp.PlanExercises)
            .HasForeignKey(pe => pe.PlanId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasOne(pe => pe.Exercise)
            .WithMany()
            .HasForeignKey(pe => pe.ExerciseId)
            .OnDelete(DeleteBehavior.Restrict); // не удаляем системные упражнения
        
        builder.Property(pe => pe.Order)
            .IsRequired()
            .HasDefaultValue(0);
        
        builder.Property(pe => pe.Sets)
            .HasDefaultValue(null);
        
        builder.Property(pe => pe.Reps)
            .HasDefaultValue(null);
        
        builder.Property(pe => pe.Notes)
            .HasMaxLength(500);
    }
}