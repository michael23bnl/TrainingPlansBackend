
using TrainingPlans.Entities;
using System.Text.Json;
namespace TrainingPlans.Extensions;

public static class SeedDataExtensions
{
    public static void SeedPlansData(this WebApplication app, string dataFilePath)
    {
        using var scope = app.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<PlansDbContext>();

        if (!context.Plans.Any())
        {
            var jsonData = File.ReadAllText(dataFilePath);
            var plans = JsonSerializer.Deserialize<List<PlanEntity>>(jsonData);
            if (plans != null)
            {
                context.AddRange(plans);
                context.SaveChanges();
            }
        }
    }

    public static void SeedExercisesData(this WebApplication app, string dataFilePath)
    {
        using var scope = app.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<PlansDbContext>();

        if (!context.Exercises.Any())
        {
            var jsonData = File.ReadAllText(dataFilePath);
            var exercises = JsonSerializer.Deserialize<List<ExerciseEntity>>(jsonData);
            if (exercises != null)
            {
                context.AddRange(exercises);
                context.SaveChanges();
            }
        }
    }
}

