
using Microsoft.EntityFrameworkCore;
using TrainingPlans.Domain.Abstractions;
using TrainingPlans.Domain.Entities;
using Shared.Pagination;
using TrainingPlans.Domain.DTO;

namespace TrainingPlans.Infrastructure.Repositories;

public class PlansRepository : IPlansRepository
{
    private readonly PlansDbContext _context;

    public PlansRepository(PlansDbContext context)
    {
        _context = context;
    }

    public async Task<Guid> CreateAsync(List<PlanExercise> exercises, string? description, CancellationToken ct)
    {
        var plan = new PlanEntity
        {
            Id = Guid.NewGuid(),
            Description = description,
            CreatedAt = DateTime.UtcNow
        };
        var order = 0;

        foreach (var exercise in exercises)
        {
            plan.PlanExercises.Add(new PlanExerciseEntity()
            {
                PlanId = plan.Id,
                ExerciseId = exercise.ExerciseId,
                Order = order++,
                Sets = exercise.Sets,
                Reps = exercise.Reps
            });
        }
        
        await _context.Plans.AddAsync(plan, ct);
        await _context.SaveChangesAsync(ct);
        
        return plan.Id;
    }
    
    public async Task<(int, List<PlanEntity>)> GetAllAsync(
        PlanParameters? planParameters, CancellationToken ct)
    {
        var totalPlanCount = await _context.Plans.CountAsync(ct);

        var query = _context.Plans
            .OrderBy(p => p.CreatedAt)
            .Include(p => p.PlanExercises
                .OrderBy(pe => pe.Order))
            .ThenInclude(pe => pe.Exercise)
            .AsNoTracking();

        if (planParameters is not null)
        {
            query = query
                .Skip((planParameters.PageNumber - 1) * planParameters.PageSize)
                .Take(planParameters.PageSize);
        }

        var plans = await query.ToListAsync(ct);

        return (totalPlanCount, plans);
    }

    public async Task<PlanEntity?> GetAsync(Guid planId, CancellationToken ct)
    {
        var plan = await _context.Plans
            .Include(p => p.PlanExercises
                .OrderBy(pe => pe.Order))
            .ThenInclude(pe => pe.Exercise)
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.Id == planId, ct);
        
       return plan;
    }

    public async Task<List<PlanEntity>> GetByIdsAsync(List<Guid> planIds, CancellationToken ct)
    {
        var plans = await _context.Plans
            .Where(p => planIds.Contains(p.Id))
            .OrderBy(p => p.CreatedAt)
            .Include(p => p.PlanExercises
                .OrderBy(pe => pe.Order))
            .ThenInclude(pe => pe.Exercise)
            .AsNoTracking()
            .ToListAsync(ct);
    
        return plans;
    }

    public async Task<Guid> UpdateAsync(Guid id, List<PlanExercise>? exercises, string? description, CancellationToken ct)
    {
        var plan = await _context.Plans
            .Include(p => p.PlanExercises)
            .FirstOrDefaultAsync(p => p.Id == id, ct);
        
        if (plan is null)
            throw new InvalidOperationException("Plan not found");
        

        plan.Description = description;
        
        if (exercises != null)
        {
            _context.PlanExercises.RemoveRange(plan.PlanExercises);
            
            var order = 0;
            foreach (var exercise in exercises)
            {
                plan.PlanExercises.Add(new PlanExerciseEntity
                {
                    PlanId = plan.Id,
                    ExerciseId = exercise.ExerciseId,
                    Order = order++,
                    Sets = exercise.Sets,
                    Reps = exercise.Reps,
                });
            }
        }

        await _context.SaveChangesAsync(ct);

        return plan.Id;
    }

    public async Task<Guid> DeleteAsync(Guid id, CancellationToken ct)
    {
        var plan = await _context.Plans
            .Include(p => p.PlanExercises)
            .FirstOrDefaultAsync(p => p.Id == id, ct);
        
        if (plan is null)
            throw new InvalidOperationException("Plan not found");
        
        
        _context.Plans.Remove(plan);
        await _context.SaveChangesAsync(ct);

        return id;
    }
}