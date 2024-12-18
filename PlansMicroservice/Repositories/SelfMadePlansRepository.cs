using Microsoft.EntityFrameworkCore;
using TrainingPlans.Entities;
using TrainingPlans.Models;

namespace TrainingPlans.Repositories;

public class SelfMadePlansRepository
{
    private readonly PlansDbContext _context;

    public SelfMadePlansRepository(PlansDbContext context)
    {
        _context = context;
    }
    
    public async Task<Guid> Create(Guid userId, PlanModel plan)
    {
        var selfMadePlanEntity = new SelfMadePlanEntity
        {
            Id = plan.Id,
            UserId = userId,
            Exercises = plan.Exercises.Select(e => new ExerciseEntity
            {
                Id = e.Id,
                Name = e.Name,
                MuscleGroup = e.MuscleGroup,
            }).ToList()
        };
        await _context.SelfMadePlans.AddAsync(selfMadePlanEntity);
        await _context.SaveChangesAsync();
        return selfMadePlanEntity.Id;
    }

    public async Task<List<SelfMadePlanModel>> GetAll(Guid userId)
    {
        var selfMadePlanEntities = await _context.SelfMadePlans
            .Include(e => e.Exercises)
            .AsNoTracking()
            .Where(s => s.UserId == userId).ToListAsync();
        
        var plans = selfMadePlanEntities.Select(p => SelfMadePlanModel.Create(p.Id, userId,
            p.Exercises.Select(e => ExerciseModel.Create(e.Id, e.Name, e.MuscleGroup).exerciseModel).ToList()!).planModel).ToList();
        return plans;
    }
    
    public async Task<SelfMadePlanModel> Get(Guid planId, Guid userId)
    {
        var selfMadePlanEntity = await _context.SelfMadePlans
            .Include(e => e.Exercises)
            .FirstOrDefaultAsync(s => s.Id == planId && s.UserId == userId);
        var plan = SelfMadePlanModel.Create(selfMadePlanEntity.Id, selfMadePlanEntity.UserId, selfMadePlanEntity.Exercises
            .Select(e => ExerciseModel.Create(e.Id, e.Name, e.MuscleGroup).exerciseModel)
            .ToList()!).planModel;
        return plan;
    }

    public async Task<Guid> Update(Guid planId, Guid userId, List<ExerciseModel> exercises)
    {
        // Загружаем план вместе с его упражнениями
        var plan = await _context.SelfMadePlans
            .Include(e => e.Exercises)
            .FirstOrDefaultAsync(s => s.Id == planId && s.UserId == userId);

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


    public async Task<Guid> Delete(Guid planId, Guid userId)
    {
        var plan = await _context.SelfMadePlans.FirstOrDefaultAsync(s => s.Id == planId && s.UserId == userId);
        if (plan != null)
        {
            _context.SelfMadePlans.Remove(plan);
            await _context.SaveChangesAsync();
        }

        return planId;
    }
}