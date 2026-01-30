using Microsoft.EntityFrameworkCore;
using TrainingPlans.Domain.Abstractions;
using TrainingPlans.Domain.Entities;

namespace TrainingPlans.Persistence.Repositories;

public class CustomPlansRepository : ICustomPlansRepository
{
    private readonly PlansDbContext _context;

    public CustomPlansRepository(PlansDbContext context)
    {
        _context = context;
    }
    
    public async Task<Guid> AddOrRemoveAsync(Guid userId, Guid planId, CancellationToken ct)
    {
        var existingPlan = await _context.CustomPlans
            .FirstOrDefaultAsync(cp => cp.UserId == userId && cp.PlanId == planId, ct);
        
        if (existingPlan == null)
        {
            var customPlan = new CustomPlanEntity()
            {
                UserId = userId,
                PlanId = planId
            };
            await _context.CustomPlans.AddAsync(customPlan);
        }
        else
        {
            _context.CustomPlans.Remove(existingPlan);
        }
        
        await _context.SaveChangesAsync();

        return planId;
    }

    public async Task<Guid> CompleteAsync(Guid userId, Guid planId, CancellationToken ct)
    {
        var customPlan = await _context.CustomPlans
            .Where(cp => cp.UserId == userId && cp.PlanId == planId)
            .FirstOrDefaultAsync(ct);

        if (customPlan is null)
        {
            throw new InvalidOperationException("Plan not found");
        }
        
        customPlan.CompletionDate = DateOnly.FromDateTime(DateTime.Now);
        
        return customPlan.PlanId;
    }

    public async Task<List<Guid>> GetCompletedIdsAsync(Guid userId, CancellationToken ct)
    {
        var ids = await _context.CustomPlans
            .AsNoTracking()
            .Where(cp => cp.UserId == userId)
            .Select(cp => cp.PlanId)
            .ToListAsync(ct);

        return ids;
    }
}