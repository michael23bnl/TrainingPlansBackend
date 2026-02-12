using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TrainingPlans.Domain.Entities;

namespace TrainingPlans.Infrastructure.Configurations;

public class PlanExerciseConfiguration : IEntityTypeConfiguration<PlanExerciseEntity>
{
    public void Configure(EntityTypeBuilder<PlanExerciseEntity> builder)
    {
        builder.HasKey(pe => new { pe.PlanId, pe.ExerciseId });
        
        builder.HasOne(pe => pe.Plan)
            .WithMany(p => p.PlanExercises)
            .HasForeignKey(pe => pe.PlanId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasOne(pe => pe.Exercise)
            .WithMany()
            .HasForeignKey(pe => pe.ExerciseId)
            .OnDelete(DeleteBehavior.Restrict);
        
        builder.Property(pe => pe.Order)
            .IsRequired()
            .HasDefaultValue(0);
    }
}