using TrainingPlans.Entities;
using TrainingPlans.Repositories.Interfaces;
using TrainingPlans.Services;

namespace TrainingPlans.Extensions;

public static class SeedElasticDataExtensions
{

    public static async Task SeedPlansData(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var elasticService = scope.ServiceProvider.GetRequiredService<IElasticService>();
        var plansRepository = scope.ServiceProvider.GetRequiredService<IPlansRepository>();

        if (!await elasticService.ContainsDocuments("plans.json"))
        {
            var planResponses = await plansRepository.GetAllPrepared();
            var planEntities = planResponses.Select(p => 
                new PlanEntity
                {
                    Id = p.Id,
                    Category = p.Category,
                    Exercises = p.Exercises.Select(e => new ExerciseEntity
                    {
                        Id = e.Id,
                        Name = e.Name,
                        MuscleGroup = e.MuscleGroup
                        //IsPreMade = e.IsPreMade
                    }).ToList(),
                    CreatedBy = p.CreatedBy
                }).ToList();
            await elasticService.AddOrUpdateBulk(planEntities);
        }
    }
    
}