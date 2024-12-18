using Microsoft.EntityFrameworkCore;
using TrainingPlans.Entities;
using TrainingPlans.Models;
using TrainingPlans.Repositories.Interfaces;

namespace TrainingPlans.Repositories;

public class PlansRepository : IPlansRepository
{
    private readonly PlansDbContext _context;

    public PlansRepository(PlansDbContext context)
    {
        _context = context;
    }

    public virtual async Task<Guid> Create(PlanModel plan)
    {
        var planEntity = new PlanEntity
        {
            Id = plan.Id,
            Exercises = plan.Exercises.Select(e => new ExerciseEntity
                {
                    Id = e.Id,
                    Name = e.Name,
                    MuscleGroup = e.MuscleGroup,
                }).ToList()
        };
        await _context.Plans.AddAsync(planEntity);
        await _context.SaveChangesAsync();
        return planEntity.Id;
    }

    public async Task<List<PlanModel>> GetAll()
    {
        var planEntities = await _context.Plans
            .Include(e => e.Exercises)
            .AsNoTracking()
            .ToListAsync();
        var plans = planEntities.Select(p => PlanModel.Create(p.Id,
            p.Exercises.Select(e => ExerciseModel.Create(e.Id, e.Name, e.MuscleGroup).exerciseModel).ToList()!).planModel).ToList();

        return plans;
    }

    public async Task<PlanModel> Get(Guid id)
    {
        var planEntity = await _context.Plans
            .Include(e => e.Exercises)
            .FirstOrDefaultAsync(p => p.Id == id);
        var plan = PlanModel.Create(planEntity.Id, planEntity.Exercises
            .Select(e => ExerciseModel.Create(e.Id, e.Name, e.MuscleGroup).exerciseModel)
            .ToList()!).planModel;
        return plan;
    }

    public async Task<Guid> Update(Guid id, List<ExerciseModel> exercises)
    {
        // Загружаем план вместе с его упражнениями
        var plan = await _context.Plans
            .Include(p => p.Exercises)
            .FirstOrDefaultAsync(p => p.Id == id);

        if (plan == null)
        {
            throw new InvalidOperationException("Plan not found");
        }

        // Создаем список ID новых упражнений из модели
        var incomingExerciseIds = exercises.Select(e => e.Id).ToHashSet();

        // Удаляем старые упражнения, которые отсутствуют в новом списке
        var exercisesToRemove = plan.Exercises
            .Where(e => !incomingExerciseIds.Contains(e.Id))
            .ToList();

        foreach (var exerciseToRemove in exercisesToRemove)
        {
            _context.Exercises.Remove(exerciseToRemove); // Удаляем из контекста
        }

        // Обновляем или добавляем новые упражнения
        foreach (var exerciseModel in exercises)
        {
            var existingExercise = plan.Exercises
                .FirstOrDefault(e => e.Id == exerciseModel.Id);

            if (existingExercise != null)
            {
                // Обновляем существующее упражнение
                existingExercise.Name = exerciseModel.Name;
                existingExercise.MuscleGroup = exerciseModel.MuscleGroup;
            }
            else
            {
                // Добавляем новое упражнение
                var newExercise = new ExerciseEntity
                {
                    Id = new Guid(), 
                    Name = exerciseModel.Name,
                    MuscleGroup = exerciseModel.MuscleGroup,
                };

                plan.Exercises.Add(newExercise); // Добавляем в план
            }
        }

        await _context.SaveChangesAsync();

        return plan.Id;
    }


    public async Task<Guid> Delete(Guid id)
    {
        var plan = await _context.Plans.FirstOrDefaultAsync(p => p.Id == id);
        if (plan != null)
        {
            _context.Plans.Remove(plan);
            await _context.SaveChangesAsync();
        }

        return id;
    }
    
}