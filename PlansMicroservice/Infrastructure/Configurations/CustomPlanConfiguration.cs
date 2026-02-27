using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TrainingPlans.Domain.Entities;

namespace TrainingPlans.Infrastructure.Configurations;

public class CustomPlanConfiguration : IEntityTypeConfiguration<CustomPlanEntity>
{
    public void Configure(EntityTypeBuilder<CustomPlanEntity> builder)
    {
        builder.HasKey(cp => cp.Id);
        
        builder.Property(cp => cp.UserId).IsRequired();
        
        builder
            .HasOne(cp => cp.SourcePlan)
            .WithMany()
            .HasForeignKey(cp => cp.SourcePlanId)
            .OnDelete(DeleteBehavior.SetNull);
        
        builder
            .HasMany(cp => cp.PlanExercises)
            .WithOne(pe => pe.CustomPlan)
            .HasForeignKey(pe => pe.PlanId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();
        
        builder.Property(cp => cp.CreatedAt).IsRequired();
        builder.Property(cp => cp.CompletionDate);

        builder.Property(cp => cp.Description).HasMaxLength(500);
    }
}