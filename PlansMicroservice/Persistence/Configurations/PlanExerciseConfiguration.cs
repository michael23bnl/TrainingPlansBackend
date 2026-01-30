using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TrainingPlans.Domain.Entities;

namespace TrainingPlans.Persistence.Configurations;

public class PlanExerciseConfiguration : IEntityTypeConfiguration<PlanExerciseEntity>
{
    public void Configure(EntityTypeBuilder<PlanExerciseEntity> builder)
    {
        builder.HasKey(pe => new { pe.PlanId, pe.ExerciseId });
        
        builder.HasOne<PlanEntity>()
            .WithMany()
            .HasForeignKey(pe => pe.PlanId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasOne<ExerciseEntity>()
            .WithMany()
            .HasForeignKey(pe => pe.ExerciseId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}