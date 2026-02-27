
using TrainingPlans.Domain.Abstractions;

namespace TrainingPlans.Infrastructure.Extensions;

public static class SeedElasticDataExtensions
{
    public static async Task SeedPlansData(this WebApplication app, CancellationToken ct)
    {
        using var scope = app.Services.CreateScope();
        var elasticAdminService = scope.ServiceProvider.GetRequiredService<IElasticAdminService>();
        var plansService = scope.ServiceProvider.GetRequiredService<IPlansService>();
        
        await elasticAdminService.CreateIndexAsync(ct);
        
        if (!await elasticAdminService.ContainsDocumentsAsync(ct))
        {
            var plans = await plansService.GetAllPlansAsync(ct);
            
            await elasticAdminService.IndexPlansAsync(plans, ct);
        }
    }
}