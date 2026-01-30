using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TrainingPlans.Domain.Entities;

namespace TrainingPlans.Persistence.Configurations;

public class PlanConfiguration : IEntityTypeConfiguration<PlanEntity>
{
    public void Configure(EntityTypeBuilder<PlanEntity> builder)
    {
        builder.HasKey(p => p.Id);
        
        builder.Property(p => p.CreatedBy);

        builder.Ignore(p => p.Tags);
        builder.Ignore(p => p.Exercises);
    }
}