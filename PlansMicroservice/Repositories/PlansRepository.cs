using Elastic.Clients.Elasticsearch.MachineLearning;
using Microsoft.EntityFrameworkCore;

using TrainingPlans.Contracts;
using TrainingPlans.Entities;
using TrainingPlans.Models;
using TrainingPlans.Pagination;
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
            Category = plan.Category,
            Exercises = plan.Exercises.Select(e => new ExerciseEntity
                {
                    Id = e.Id,
                    Name = e.Name,
                    MuscleGroup = e.MuscleGroup,
                    //IsPreMade = e.IsPreMade
                }).ToList(),
            CreatedBy = plan.CreatedBy
        };
        await _context.Plans.AddAsync(planEntity);
        await _context.SaveChangesAsync();
        return planEntity.Id;
    }

    public async Task<List<PlanModel>> GetAll()
    {
        var planEntities = await _context.Plans
            //.Include(e => e.Exercises)
            .AsNoTracking()
            .ToListAsync();
        var plans = planEntities.Select(p => PlanModel.Create(p.Id, p.Category,
            p.Exercises
                //.OrderBy(e => e.CreatedAt)
                .Select(e => ExerciseModel.Create(e.Id, e.Name, e.MuscleGroup)
                    .exerciseModel).ToList()!, p.CreatedBy).planModel).ToList();

        return plans;
    }

    public async Task<List<Guid>> GetFavoritePlanIds(Guid userId)
    {
        var favoritePlanIds = _context.FavoritePlans
            .Where(p => p.UserId == userId)
            .Select(p => p.PlanId).ToList();
        
        return favoritePlanIds;
    }

    public async Task<List<Guid>> GetCompletedPlanIds(Guid userId)
    {
        var completedPlanIds = _context.CompletedPlans.Where(p => p.UserId == userId)
            .Where(p => p.UserId == userId)
            .Select(p => p.PlanId).ToList();
        
        return completedPlanIds;
    }

    public async Task<List<PreparedPlanResponse>> GetAllAvailable(Guid userId)
    {
        var availablePlansUnmarked = await _context.Plans
            //.Include(e => e.Exercises)
            .Where(p => p.CreatedBy == null || p.CreatedBy == userId)
            .AsNoTracking()
            .ToListAsync();
        
        var favoritePlanIds = (await _context.FavoritePlans
                .Where(p => p.UserId == userId)
                .Select(f => f.PlanId)
                .ToListAsync())
            .ToHashSet();
        
        var availablePlans = availablePlansUnmarked.Select(p => new PreparedPlanResponse(
            p.Id,
            p.Category,
            p.Exercises
                //.OrderBy(e => e.CreatedAt)
                .Select(e => new ExerciseResponse(
                    e.Id,
                    e.Name,
                    e.MuscleGroup
                )).ToList(),
            favoritePlanIds.Contains(p.Id)
        )).ToList();
        

        return availablePlans;
    }
    
    public async Task<List<PlanModel>> GetAllPrepared() // для неавторизованных пользователей
    {
        var planEntities = await _context.Plans
            //.Include(e => e.Exercises)
            .Where(p => p.CreatedBy == null)
            .AsNoTracking()
            .ToListAsync();
        var plans = planEntities.Select(p => PlanModel.Create(
            p.Id,
            p.Category,
            p.Exercises.Select(e => ExerciseModel.Create(
                e.Id,
                e.Name,
                e.MuscleGroup
                //e.IsPreMade
                ).exerciseModel).ToList()!,
            p.CreatedBy)
            .planModel).ToList();

        return plans;
    }
    
    public async Task<(int, List<PreparedPlanResponse>)> GetAllPrepared(Guid? userId,
        PlanParameters planParameters) // для авторизованных пользователей
    {
        var totalEntityCount = _context.Plans.Count(p => p.CreatedBy == null);
        
        var planEntities = await _context.Plans
            .Where(p => p.CreatedBy == null)
            .Skip((planParameters.PageNumber - 1) * planParameters.PageSize)
            .Take(planParameters.PageSize)
            .AsNoTracking()
            .ToListAsync();

        var favoritePlanIds = _context.FavoritePlans
            .Where(f => f.UserId == userId)
            .Select(f => f.PlanId)
            .ToList();
        
        var plans = planEntities.Select(p => new PreparedPlanResponse(
            p.Id,
            p.Category,
            p.Exercises
                //.OrderBy(e => e.CreatedAt)
                .Select(e => new ExerciseResponse(
                e.Id,
                e.Name,
                e.MuscleGroup
            )).ToList(),
            favoritePlanIds.Contains(p.Id)
        )).ToList();

        return (totalEntityCount, plans);
    }
    
    public async Task<(int, List<CompletedPlanResponse>)> GetAllSelfMade(Guid userId, PlanParameters planParameters)
    {
        var totalEntityCount = _context.Plans.Count(p => p.CreatedBy == userId);
        
        var planEntities = await _context.Plans
            .Where(p => p.CreatedBy == userId)
            .Skip((planParameters.PageNumber - 1) * planParameters.PageSize)
            .Take(planParameters.PageSize)
            .AsNoTracking()
            .ToListAsync();
        
        var completedPlanIds = _context.CompletedPlans
            .Where(f => f.UserId == userId)
            .Select(f => f.PlanId)
            .ToList();
        
        var plans = planEntities.Select(p => new CompletedPlanResponse(
            p.Id,
            p.Category,
            p.Exercises
                .Select(e => new ExerciseResponse(
                    e.Id,
                    e.Name,
                    e.MuscleGroup
                )).ToList(),
            completedPlanIds.Contains(p.Id)
        )).ToList();

        return (totalEntityCount, plans);
    }

    public async Task<PreparedPlanResponse> Get(Guid planId, Guid? userId)
    {
        var planEntity = await _context.Plans
            .FirstOrDefaultAsync(p => p.Id == planId);

        var isFavorite = await _context.FavoritePlans
            .AnyAsync(f => f.UserId == userId && f.PlanId == planId);
        
        var plan = new PreparedPlanResponse(
            planEntity.Id,
            planEntity.Category,
            planEntity.Exercises
                .Select(e => new ExerciseResponse(
                    e.Id,
                    e.Name,
                    e.MuscleGroup
                )).ToList(),
            isFavorite);
        
        /*var plan = PlanModel.Create(planEntity.Id, planEntity.Category, planEntity.Exercises
            .Select(e => ExerciseModel.Create(e.Id, e.Name, e.MuscleGroup, e.IsPreMade).exerciseModel)
            .ToList()!, planEntity.CreatedBy).planModel;*/
        return plan;
    }
    
    public async Task<PlanModel> GetByName(Guid userId, string name)
    {
        var planEntity = await _context.Plans
            .Include(pe => pe.Exercises)
            .AsNoTracking()
            .FirstOrDefaultAsync(pe => pe.Category == name && pe.CreatedBy == userId);
        
        var plan = PlanModel.Create(planEntity.Id, planEntity.Category,
            planEntity.Exercises
                .Select(pe => 
                    ExerciseModel.Create(
                        pe.Id, 
                        pe.Name, 
                        pe.MuscleGroup
                        //pe.IsPreMade
                            )
                        .exerciseModel)
                .ToList(), 
            planEntity.CreatedBy).planModel;
        
        return plan;
    }
    
    public async Task<PlanModel> GetPreparedByName(string name)
    {
        var planEntity = await _context.Plans
            .Include(pe => pe.Exercises)
            .AsNoTracking()
            .FirstOrDefaultAsync(pe => pe.Category == name && pe.CreatedBy == null);
        
        var plan = PlanModel.Create(planEntity.Id, planEntity.Category,
            planEntity.Exercises
                .Select(pe => 
                    ExerciseModel.Create(
                            pe.Id, 
                            pe.Name, 
                            pe.MuscleGroup 
                            //pe.IsPreMade
                            )
                        .exerciseModel)
                .ToList(), 
            planEntity.CreatedBy).planModel;
        
        return plan;
    }

    public async Task<Guid> Update(Guid id, string? category, List<ExerciseModel> exercises)
    {
        var plan = await _context.Plans
            //.Include(p => p.Exercises)
            .FirstOrDefaultAsync(p => p.Id == id);
        
        if (plan == null)
        {
            throw new InvalidOperationException("Plan not found");
        }

        if (exercises.Count == 0)
        {
            throw new InvalidOperationException("Plan must contain at least one exercise");
        }

        plan.Category = category;
        
        plan.Exercises = exercises.Select(e => new ExerciseEntity
        {
            Id = e.Id,
            Name = e.Name,
            MuscleGroup = e.MuscleGroup
        }).ToList();

        await _context.SaveChangesAsync();

        return plan.Id;
    }


    public async Task<Guid> Delete(Guid id)
    {
        var plan = await _context.Plans
            //.Include(p => p.Exercises)
            .FirstOrDefaultAsync(p => p.Id == id);
        if (plan != null)
        {
            _context.Plans.Remove(plan);
            await _context.SaveChangesAsync();
        }

        return id;
    }
    
}