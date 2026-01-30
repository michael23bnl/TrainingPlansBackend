using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TrainingPlans.Domain.Entities;
using UserMicroservice.Entities;

namespace TrainingPlans.Persistence.Configurations;

public class CustomPlanConfiguration : IEntityTypeConfiguration<CustomPlanEntity>
{
    public void Configure(EntityTypeBuilder<CustomPlanEntity> builder)
    {
        builder.HasKey(cp => new { cp.PlanId, cp.UserId });
        
        builder.HasOne<PlanEntity>()
            .WithMany()
            .HasForeignKey(cp => cp.PlanId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasOne<UserEntity>()
            .WithMany()
            .HasForeignKey(cp => cp.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}