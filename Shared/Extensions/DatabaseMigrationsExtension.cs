using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Shared.Extensions;

public static class DatabaseMigrationsExtension
{
    
    public static WebApplication ApplyDatabaseMigrations<TDbContext>(this WebApplication app)
        where TDbContext : DbContext
    {
        using var scope = app.Services.CreateScope();
        
        var services = scope.ServiceProvider;

        var logger = services.GetRequiredService<ILogger<TDbContext>>();
        var dbContext = services.GetRequiredService<TDbContext>();

        try
        {
            dbContext.Database.Migrate();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Migration failed for {DbContext}", typeof(TDbContext).Name);
            throw;
        }

        return app;
    }
}