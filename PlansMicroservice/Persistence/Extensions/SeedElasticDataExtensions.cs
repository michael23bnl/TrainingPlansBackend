using TrainingPlans.Application.Services.Interfaces;
using TrainingPlans.Persistence.Repositories.Interfaces;

namespace TrainingPlans.Persistence.Extensions;

public static class SeedElasticDataExtensions
{

    public static async Task SeedPlansData(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var elasticService = scope.ServiceProvider.GetRequiredService<IPlansSearch>();
        var plansRepository = scope.ServiceProvider.GetRequiredService<IPlansRepository>();

        if (!await elasticService.ContainsDocuments("plans.json"))
        {
            var planModels = await plansRepository.GetAllPrepared();
            await elasticService.AddOrUpdateBulk(planModels);
        }
    }
    
}