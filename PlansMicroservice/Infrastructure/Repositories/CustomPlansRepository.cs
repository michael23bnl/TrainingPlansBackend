using Microsoft.EntityFrameworkCore;
using TrainingPlans.Domain.Abstractions;
using TrainingPlans.Domain.DTO;
using TrainingPlans.Domain.Entities;

namespace TrainingPlans.Infrastructure.Repositories;

public class CustomPlansRepository : ICustomPlansRepository
{
    private readonly PlansDbContext _context;

    public CustomPlansRepository(PlansDbContext context)
    {
        _context = context;
    }
    
    public async Task<Guid> CreateAsync(Guid userId, string? description, Guid? sourcePlanId, 
        List<CustomPlanExercise> exercises, CancellationToken ct)
    {
        var plan = new CustomPlanEntity
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            SourcePlanId = sourcePlanId,
            Description = description,
            CreatedAt = DateTime.UtcNow
        };

        var order = 0;

        foreach (var exercise in exercises)
        {
            plan.PlanExercises.Add(new CustomPlanExerciseEntity
            {
                PlanId = plan.Id,
                ExerciseId = exercise.ExerciseId,
                Order = order++,
                Sets = exercise.Sets,
                Reps = exercise.Reps,
                Notes = exercise.Notes
            });
        }
        
        await _context.CustomPlans.AddAsync(plan, ct);
        await _context.SaveChangesAsync(ct);

        return plan.Id;
    }

    public async Task<List<CustomPlanEntity>> GetAllAsync(Guid userId, CancellationToken ct)
    {
        var plans = await _context.CustomPlans
            .Where(cp => cp.UserId == userId)
            .OrderBy(p => p.CreatedAt)
            .Include(cp => cp.PlanExercises
                .OrderBy(cpe => cpe.Order))
            .AsNoTracking()
            .ToListAsync(ct);

        return plans;
    }
    
    public async Task<List<CustomPlanEntity>> GetCompletedAsync(Guid userId, CancellationToken ct)
    {
        var plans = await _context.CustomPlans
            .Where(cp => cp.UserId == userId && cp.CompletionDate != null)
            .OrderBy(p => p.CreatedAt)
            .Include(cp => cp.PlanExercises
                .OrderBy(cpe => cpe.Order))
            .AsNoTracking()
            .ToListAsync(ct);

        return plans;
    }

    public async Task<Guid> UpdateAsync(Guid userId, Guid planId, string? description, 
        List<CustomPlanExercise>? exercises, CancellationToken ct)
    {
        var plan = await _context.CustomPlans
            .Include(cp => cp.PlanExercises)
            .FirstOrDefaultAsync(cp => cp.UserId == userId && cp.Id == planId, ct);
        
        if (plan is null)
            throw new InvalidOperationException("Plan not found");
        
        plan.Description = description;

        if (exercises != null)
        {
            _context.CustomPlanExercises.RemoveRange(plan.PlanExercises);

            var order = 0;

            foreach (var exercise in exercises)
            {
                plan.PlanExercises.Add(new CustomPlanExerciseEntity
                {
                    PlanId = plan.Id,
                    ExerciseId = exercise.ExerciseId,
                    Order = order++,
                    Sets = exercise.Sets,
                    Reps = exercise.Reps,
                    Notes = exercise.Notes
                });
            }
        }
        
        await _context.SaveChangesAsync(ct);

        return planId;
    }
    
    public async Task<Guid> DeleteAsync(Guid userId, Guid planId, CancellationToken ct)
    {
        var plan = await _context.CustomPlans
            .Where(cp => cp.UserId == userId)
            .Include(cp => cp.PlanExercises)
            .FirstOrDefaultAsync(cp => cp.Id == planId, ct);

        if (plan is null)
            throw new InvalidOperationException("Plan not found");


        _context.CustomPlans.Remove(plan);
        await _context.SaveChangesAsync(ct);

        return planId;
    }
    
    public async Task<Guid> CompleteAsync(Guid userId, Guid planId, CancellationToken ct)
    {
        var affectedRows = await _context.CustomPlans
            .Where(cp => cp.UserId == userId && cp.Id == planId)
            .ExecuteUpdateAsync(s => s
                .SetProperty(cp => cp.CompletionDate, DateOnly.FromDateTime(DateTime.Now)), ct);

        if (affectedRows == 0)
            throw new InvalidOperationException("Plan not found");
        
        return planId;
    }
    
    public async Task<Guid> UncompleteAsync(Guid userId, Guid planId, CancellationToken ct)
    {
        var affectedRows = await _context.CustomPlans
            .Where(cp => cp.UserId == userId && cp.Id == planId)
            .ExecuteUpdateAsync(s => s
                .SetProperty(cp => cp.CompletionDate, (DateOnly?)null), ct);

        if (affectedRows == 0)
            throw new InvalidOperationException("Plan not found");
        
        return planId;
    }
}