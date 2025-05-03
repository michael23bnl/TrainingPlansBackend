using Microsoft.EntityFrameworkCore;
using TrainingPlans.Entities;
using TrainingPlans.Models;
using TrainingPlans.Repositories.Interfaces;

namespace TrainingPlans.Repositories;

public class CompletedPlansRepository : ICompletedPlansRepository
{
    private readonly PlansDbContext _context;

    public CompletedPlansRepository(PlansDbContext context)
    {
        _context = context;
    }
    
    public async Task MarkAsCompleted(Guid userId, Guid planId)
    {
        var existingCompletedPlan = await _context.CompletedPlans
            .FirstOrDefaultAsync(f => f.UserId == userId && f.PlanId == planId);
        if (existingCompletedPlan == null)
        {
            var completedPlan = new CompletedPlanEntity()
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                PlanId = planId,
            };
            await _context.CompletedPlans.AddAsync(completedPlan);
            await _context.SaveChangesAsync();
        }
        else
        {
            _context.CompletedPlans.Remove(existingCompletedPlan);
            await _context.SaveChangesAsync();
        }
    }

    public async Task RemoveCompletedMark(Guid userId, Guid planId)
    {
        var completedPlan = await _context.CompletedPlans
            .FirstOrDefaultAsync(f => f.UserId == userId && f.PlanId == planId);

        _context.CompletedPlans.Remove(completedPlan);
        await _context.SaveChangesAsync();
    }

    public async Task<List<PlanModel>> GetCompletedPlans(Guid userId)
    {
        var completedPlans = _context.CompletedPlans
            .Where(f => f.UserId == userId)
            .Select(f => f.PlanId);

        var planEntities = await _context.Plans
            .Where(p => completedPlans.Contains(p.Id))
            .ToListAsync();
        
        var plans = planEntities.Select(p => PlanModel.Create(
            p.Id, 
            p.Category,
            p.Exercises
                .Select(e => ExerciseModel.Create(
                e.Id, 
                e.Name, 
                e.MuscleGroup
                ).exerciseModel).ToList()!, 
            p.CreatedBy).planModel).ToList();

        return plans;
    }
}