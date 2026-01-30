
using TrainingPlans.Application.Abstractions;

namespace TrainingPlans.Infrastructure.Extensions;

public static class SeedElasticDataExtensions
{

    public static async Task SeedPlansData(this WebApplication app, CancellationToken ct)
    {
        using var scope = app.Services.CreateScope();
        var elasticService = scope.ServiceProvider.GetRequiredService<IElasticService>();

        if (!await elasticService.ContainsDocumentsAsync("plans.json", ct))
        {
            await elasticService.AddOrUpdateBulkAsync(ct);
        }
    }
    
}