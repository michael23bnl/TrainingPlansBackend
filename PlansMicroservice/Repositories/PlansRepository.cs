using Microsoft.EntityFrameworkCore;

using TrainingPlans.Contracts;
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
            Name = plan.Name,
            Exercises = plan.Exercises.Select(e => new ExerciseEntity
                {
                    Id = e.Id,
                    Name = e.Name,
                    MuscleGroup = e.MuscleGroup,
                    IsPrepared = e.IsPrepared,
                    CreatedBy = e.CreatedBy
                }).ToList(),
            IsPrepared = plan.IsPrepared,
            CreatedBy = plan.CreatedBy
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
        var plans = planEntities.Select(p => PlanModel.Create(p.Id, p.Name,
            p.Exercises
                .OrderBy(e => e.CreatedAt)
                .Select(e => ExerciseModel.Create(e.Id, e.Name, e.MuscleGroup, e.IsPrepared, e.CreatedBy).exerciseModel).ToList()!, p.IsPrepared, p.CreatedBy).planModel).ToList();

        return plans;
    }
    
    public async Task<List<PlanModel>> GetAllPrepared()
    {
        var planEntities = await _context.Plans
            .Include(e => e.Exercises)
            .Where(p => p.IsPrepared == true)
            .AsNoTracking()
            .ToListAsync();
        var plans = planEntities.Select(p => PlanModel.Create(
            p.Id,
            p.Name,
            p.Exercises.Select(e => ExerciseModel.Create(
                e.Id,
                e.Name,
                e.MuscleGroup,
                e.IsPrepared,
                e.CreatedBy
                ).exerciseModel).ToList()!,
            p.IsPrepared,
            p.CreatedBy)
            .planModel).ToList();

        return plans;
    }
    
    public async Task<List<PreparedPlanResponse>> GetAllPrepared(Guid? userId)
    {
        var planEntities = await _context.Plans
            .Include(e => e.Exercises)
            .Where(p => p.IsPrepared == true)
            .AsNoTracking()
            .ToListAsync();

        var favoritePlans = _context.FavoritePlans
            .Where(f => f.UserId == userId)
            .Select(f => f.PlanId)
            .ToList();
        var plans = planEntities.Select(p => new PreparedPlanResponse(
            p.Id,
            p.Name,
            p.Exercises
                .OrderBy(e => e.CreatedAt)
                .Select(e => new ExerciseResponse(
                e.Id,
                e.Name,
                e.MuscleGroup
            )).ToList(),
            favoritePlans.Contains(p.Id)
        )).ToList();

        return plans;
    }
    
    public async Task<List<PlanModel>> GetAllSelfMade(Guid userId)
    {
        var planEntities = await _context.Plans
            .Include(e => e.Exercises)
            .Where(p => p.CreatedBy == userId)
            .AsNoTracking()
            .ToListAsync();
        var plans = planEntities.Select(p => PlanModel.Create(p.Id, p.Name,
            p.Exercises
                .OrderBy(e => e.CreatedAt)
                .Select(e => ExerciseModel.Create(e.Id, e.Name, e.MuscleGroup, e.IsPrepared, e.CreatedBy).exerciseModel).ToList()!, p.IsPrepared, p.CreatedBy).planModel).ToList();

        return plans;
    }

    public async Task<PlanModel> Get(Guid id)
    {
        var planEntity = await _context.Plans
            .Include(e => e.Exercises)
            .FirstOrDefaultAsync(p => p.Id == id);
        var plan = PlanModel.Create(planEntity.Id, planEntity.Name, planEntity.Exercises
            .Select(e => ExerciseModel.Create(e.Id, e.Name, e.MuscleGroup, e.IsPrepared, e.CreatedBy).exerciseModel)
            .ToList()!, planEntity.IsPrepared, planEntity.CreatedBy).planModel;
        return plan;
    }
    
    public async Task<PlanModel> GetByName(Guid userId, string name)
    {
        var planEntity = await _context.Plans
            .Include(pe => pe.Exercises)
            .AsNoTracking()
            .FirstOrDefaultAsync(pe => pe.Name == name && pe.CreatedBy == userId);
        
        var plan = PlanModel.Create(planEntity.Id, planEntity.Name,
            planEntity.Exercises
                .Select(pe => 
                    ExerciseModel.Create(
                        pe.Id, 
                        pe.Name, 
                        pe.MuscleGroup, 
                        pe.IsPrepared, 
                        pe.CreatedBy)
                        .exerciseModel)
                .ToList(), 
            planEntity.IsPrepared, 
            planEntity.CreatedBy).planModel;
        
        return plan;
    }
    
    public async Task<PlanModel> GetPreparedByName(string name)
    {
        var planEntity = await _context.Plans
            .Include(pe => pe.Exercises)
            .AsNoTracking()
            .FirstOrDefaultAsync(pe => pe.Name == name && pe.IsPrepared == true);
        
        var plan = PlanModel.Create(planEntity.Id, planEntity.Name,
            planEntity.Exercises
                .Select(pe => 
                    ExerciseModel.Create(
                            pe.Id, 
                            pe.Name, 
                            pe.MuscleGroup, 
                            pe.IsPrepared, 
                            pe.CreatedBy)
                        .exerciseModel)
                .ToList(), 
            planEntity.IsPrepared, 
            planEntity.CreatedBy).planModel;
        
        return plan;
    }

    public async Task<Guid> Update(Guid id, string? name, List<ExerciseModel> exercises)
    {
        // Загружаем план вместе с его упражнениями
        var plan = await _context.Plans
            .Include(p => p.Exercises)
            .FirstOrDefaultAsync(p => p.Id == id);
        
        if (plan == null)
        {
            throw new InvalidOperationException("Plan not found");
        }

        if (exercises.Count == 0)
        {
            throw new InvalidOperationException("Plan must contain at least one exercise");
        }

        if (name != null)
        {
            plan.Name = name;
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
        var plan = await _context.Plans
            .Include(p => p.Exercises)
            .FirstOrDefaultAsync(p => p.Id == id);
        if (plan != null)
        {
            _context.Plans.Remove(plan);
            await _context.SaveChangesAsync();
        }

        return id;
    }
    
}