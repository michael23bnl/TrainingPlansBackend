using Microsoft.EntityFrameworkCore;

using TrainingPlans.Entities;
using TrainingPlans.Repositories.Interfaces;
using TrainingPlans.Models;
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

    public async Task<List<PlanModel>> GetFavorites(Guid userId)
    {
        var favoritePlans = _context.FavoritePlans
            .Where(f => f.UserId == userId)
            .Select(f => f.PlanId);

        var planEntities = await _context.Plans
            .Include(p => p.Exercises)
            .Where(p => favoritePlans.Contains(p.Id))
            .ToListAsync();
        
        var plans = planEntities.Select(p => PlanModel.Create(p.Id, p.Name,
            p.Exercises.Select(e => ExerciseModel.Create(e.Id, e.Name, e.MuscleGroup, e.IsPrepared, e.CreatedBy).exerciseModel).ToList()!, p.IsPrepared, p.CreatedBy).planModel).ToList();

        return plans;
    }

    public async Task EditFavorite(Guid userId, Guid planId, string? name, List<ExerciseModel> exercises)
    {
        
        var plan = new PlanEntity
        {
            Id = Guid.NewGuid(),
            Name = name,
            Exercises = exercises.Select(e => 
                new ExerciseEntity
                {
                    Id = e.Id,
                    Name = e.Name,
                    MuscleGroup = e.MuscleGroup,
                    IsPrepared = false,
                    CreatedBy = null
                }).ToList(),
            IsPrepared = false,
            CreatedBy = userId
        };
        
        var favoritePlan = await _context.FavoritePlans
            .FirstOrDefaultAsync(fp => fp.UserId == userId && fp.PlanId == planId);

        favoritePlan.PlanId = plan.Id;
        _context.Plans.Add(plan);
        await _context.SaveChangesAsync();

    }
    
}