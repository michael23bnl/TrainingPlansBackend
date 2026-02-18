
using TrainingPlans.Domain.Entities;
using System.Text.Json;
using TrainingPlans.Domain.DTO;
using TrainingPlans.Infrastructure.DTO;

namespace TrainingPlans.Infrastructure.Extensions;

public static class SeedPostgresDataExtensions
{
    public static void SeedPlansData(this WebApplication app, string dataFilePath)
    {
        using var scope = app.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<PlansDbContext>();
        
        if (context.Plans.Any())
            return;

        var jsonData = File.ReadAllText(dataFilePath);
        var planDtos = JsonSerializer.Deserialize<List<PlanSeed>>(jsonData);

        if (planDtos is null)
            return;
        
        foreach (var planDto in planDtos)
        {
            var plan = new PlanEntity
            {
                Id = Guid.NewGuid(),
                CreatedAt = DateTime.UtcNow,
                PlanExercises = new List<PlanExerciseEntity>()
            };
            var order = 0;

            foreach (var exerciseDto in planDto.Exercises)
            {
                var exercise = context.Exercises
                    .FirstOrDefault(e => e.Name == exerciseDto.Name);

                if (exercise is null)
                {
                    exercise = new ExerciseEntity()
                    {
                        Id = Guid.NewGuid(),
                        Name = exerciseDto.Name,
                        MuscleGroup = exerciseDto.MuscleGroup != null ? exerciseDto.MuscleGroup : "",
                        Description = string.Empty
                    };

                    context.Exercises.Add(exercise);
                }

                plan.PlanExercises.Add(new PlanExerciseEntity
                {
                    PlanId = plan.Id,
                    Plan = plan,
                    ExerciseId = exercise.Id,
                    Exercise = exercise,
                    Order = order++
                });
            }

            context.Plans.Add(plan);
        }

        context.SaveChanges();
    }

    public static void SeedExercisesData(this WebApplication app, string dataFilePath)
    {
        using var scope = app.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<PlansDbContext>();
        
        if (context.Plans.Any())
            return;

        var jsonData = File.ReadAllText(dataFilePath);
        var exerciseDtos = JsonSerializer.Deserialize<List<ExerciseSeed>>(jsonData);

        if (exerciseDtos is null)
            return;

        foreach (var exerciseDto in exerciseDtos)
        {
            var exercise = new ExerciseEntity
            {
                Id = Guid.NewGuid(),
                Name = exerciseDto.Name,
                MuscleGroup = exerciseDto.MuscleGroup,
                Description = string.Empty
            };
            
            context.Exercises.Add(exercise);
        }
        
        context.SaveChanges();
    }
}

