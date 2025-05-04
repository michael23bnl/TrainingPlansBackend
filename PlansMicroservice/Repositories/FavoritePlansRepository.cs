using Microsoft.EntityFrameworkCore;
using TrainingPlans.Contracts;
using TrainingPlans.Entities;
using TrainingPlans.Repositories.Interfaces;
using TrainingPlans.Models;
using TrainingPlans.Pagination;

namespace TrainingPlans.Repositories;

public class FavoritePlansRepository : IFavoritePlansRepository
{

    private readonly PlansDbContext _context;

    public FavoritePlansRepository(PlansDbContext context)
    {
        _context = context;
    }
    
    public async Task AddToFavorites(Guid userId, Guid planId)
    {
        var existingFavoritePlan = await _context.FavoritePlans
            .FirstOrDefaultAsync(f => f.UserId == userId && f.PlanId == planId);
        if (existingFavoritePlan == null)
        {
            var favoritePlan = new FavoritePlanEntity()
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                PlanId = planId
            };
            await _context.FavoritePlans.AddAsync(favoritePlan);
            await _context.SaveChangesAsync();
        }
        else
        {
            _context.FavoritePlans.Remove(existingFavoritePlan);
            await _context.SaveChangesAsync();
        }
    }

    public async Task RemoveFromFavorites(Guid userId, Guid planId)
    {
        var favoritePlan = await _context.FavoritePlans
            .FirstOrDefaultAsync(f => f.UserId == userId && f.PlanId == planId);

        _context.FavoritePlans.Remove(favoritePlan);
        await _context.SaveChangesAsync();
    }

    /*public async Task<PlanModel> GetFavorite(Guid userId, Guid planId)
    {
        var favoritePlan = _context.FavoritePlans
            .Where(f => f.UserId == userId && f.PlanId == planId)
            .Select(f => f.PlanId);
    }*/

    public async Task<(int, List<CompletedPlanResponse>)> GetFavorites(Guid userId, PlanParameters planParameters)
    {
        var favoritePlanIds = _context.FavoritePlans
            .Where(f => f.UserId == userId)
            .Select(f => f.PlanId);

        var totalEntityCount = favoritePlanIds.Count();
        
        var completedPlanIds = _context.CompletedPlans.Where(f => f.UserId == userId)
            .Where(f => f.UserId == userId)
            .Select(f => f.PlanId);

        var planEntities = await _context.Plans
            .Where(p => favoritePlanIds.Contains(p.Id))
            .Skip((planParameters.PageNumber - 1) * planParameters.PageSize)
            .Take(planParameters.PageSize)
            .ToListAsync();
        
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

    public async Task<PlanEntity> GetFavorite(Guid planId)
    {

        var planEntity = await _context.Plans
            .Where(p => p.Id == planId).FirstOrDefaultAsync();
        
        
        return planEntity;
    }

    public async Task<Guid> EditFavorite(Guid userId, Guid planId, string? category, List<ExerciseModel> exercises)
    {
        
        var plan = new PlanEntity
        {
            Id = Guid.NewGuid(),
            Category = category,
            Exercises = exercises.Select(e => 
                new ExerciseEntity
                {
                    Id = e.Id,
                    Name = e.Name,
                    MuscleGroup = e.MuscleGroup,
                }).ToList(),
            CreatedBy = userId
        };
        
        var favoritePlan = await _context.FavoritePlans
            .FirstOrDefaultAsync(fp => fp.UserId == userId && fp.PlanId == planId);
        
        _context.FavoritePlans.Remove(favoritePlan);
        //favoritePlan.PlanId = plan.Id;
        _context.Plans.Add(plan);
        await _context.SaveChangesAsync();
        return plan.Id;

    }
    
}