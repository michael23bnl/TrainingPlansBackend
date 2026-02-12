using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TrainingPlans.Domain.Entities;
using UserMicroservice.Entities;

namespace TrainingPlans.Infrastructure.Configurations;

public class CustomPlanConfiguration : IEntityTypeConfiguration<CustomPlanEntity>
{
    public void Configure(EntityTypeBuilder<CustomPlanEntity> builder)
    {
        builder.HasKey(cp => cp.Id);
        
        builder.HasOne<UserEntity>()
            .WithMany()
            .HasForeignKey(cp => cp.UserId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasOne(cp => cp.SourcePlan)
            .WithMany()
            .HasForeignKey(cp => cp.SourcePlanId)
            .OnDelete(DeleteBehavior.SetNull);
        
        builder.HasMany(cp => cp.PlanExercises)
            .WithOne(pe => pe.CustomPlan)
            .HasForeignKey(pe => pe.PlanId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.Property(p => p.CreatedAt);

        builder.Property(cp => cp.Description)
            .HasMaxLength(500);
    }
}