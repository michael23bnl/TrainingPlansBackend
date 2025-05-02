using Microsoft.AspNetCore.Mvc;
using TrainingPlans.Contracts;
using TrainingPlans.Repositories.Interfaces;
using TrainingPlans.Models;

namespace TrainingPlans.Controllers;

[ApiController]
[Route("/api/[controller]")]

public class FavoritePlansController : ControllerBase
{
    private readonly IFavoritePlansRepository _favoritePlansRepository;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public FavoritePlansController(IFavoritePlansRepository favoritePlansRepository, 
        IHttpContextAccessor httpContextAccessor)
    {
        _favoritePlansRepository = favoritePlansRepository;
        _httpContextAccessor = httpContextAccessor;
    }
    
    private string? GetUserId()
    {
        var userId = _httpContextAccessor.HttpContext!.Request.Headers["X-User-Id"];
        return userId;
    }
    
    [HttpPost("add/{planId}")]
    public async Task<ActionResult> AddPlanToFavorites(Guid planId)
    {
        await _favoritePlansRepository.AddToFavorites(Guid.Parse(GetUserId()!), planId);
        return Ok();
    }
    [HttpDelete("remove/{planId}")]
    public async Task<ActionResult> RemovePlanFromFavorites(Guid planId)
    {
        await _favoritePlansRepository.RemoveFromFavorites(Guid.Parse(GetUserId()!), planId);
        return Ok();
    }
    [HttpGet("get/all")]
    public async Task<ActionResult<List<PlanModel>>> GetFavoritePlans()
    {
        return Ok(await _favoritePlansRepository.GetFavorites(Guid.Parse(GetUserId()!)));
    }
    
    [HttpPut("edit/{planId}")]
    public async Task<ActionResult> EditFavoritePlan(Guid planId, [FromBody] PlanRequest request)
    {
        
        var exercises = request.Exercises.Select(e => ExerciseModel.Create(
            Guid.NewGuid(), 
            e.Name,
            e.MuscleGroup
            //false
        ).exerciseModel).ToList();

        await _favoritePlansRepository.EditFavorite(Guid.Parse(GetUserId()!), planId, request.Category, exercises);
        
        return Ok();
    }
    
}