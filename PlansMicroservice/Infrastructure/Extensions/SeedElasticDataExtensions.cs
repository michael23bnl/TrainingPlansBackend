
using TrainingPlans.Domain.Abstractions;
using TrainingPlans.Infrastructure.Elasticsearch;

namespace TrainingPlans.Infrastructure.Extensions;

public static class SeedElasticDataExtensions
{
    public static async Task SeedPlansData(this WebApplication app, CancellationToken ct)
    {
        using var scope = app.Services.CreateScope();
        var elasticAdminService = scope.ServiceProvider.GetRequiredService<IElasticAdmin>();
        var plansService = scope.ServiceProvider.GetRequiredService<IPlansService>();
        
        await elasticAdminService.CreateIndexIfNotExistsAsync(ct);
        
        if (!await elasticAdminService.ContainsDocumentsAsync(ct))
        {
            var plans = await plansService.GetAllPlansAsync(ct);
            
            await elasticAdminService.AddOrUpdateBulkAsync(plans, ct);
        }
    }
}