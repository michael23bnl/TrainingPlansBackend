using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TrainingPlans.Contracts;
using TrainingPlans.Repositories.Interfaces;
using TrainingPlans.Models;
using TrainingPlans.Pagination;
using TrainingPlans.Services;

namespace TrainingPlans.Controllers;

[ApiController]
[Route("/api/[controller]")]

public class FavoritePlansController : ControllerBase
{
    private readonly IFavoritePlansRepository _favoritePlansRepository;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IElasticService _elasticService;

    public FavoritePlansController(IFavoritePlansRepository favoritePlansRepository, 
        IHttpContextAccessor httpContextAccessor,
        IElasticService elasticService)
    {
        _favoritePlansRepository = favoritePlansRepository;
        _httpContextAccessor = httpContextAccessor;
        _elasticService = elasticService;
    }
    
    private string? GetUserId()
    {
        var userId = _httpContextAccessor.HttpContext!.Request.Headers["X-User-Id"];
        return userId;
    }
    [Authorize]
    [HttpPost("add/{planId}")]
    public async Task<ActionResult> AddPlanToFavorites(Guid planId)
    {
        await _favoritePlansRepository.AddToFavorites(Guid.Parse(GetUserId()!), planId);
        
        //var plan = await _favoritePlansRepository.GetFavorite(planId);
        
        //var result = await _elasticService.AddOrUpdateAsync(plan);
        return Ok();
    }
    
    [Authorize]
    [HttpDelete("remove/{planId}")]
    public async Task<ActionResult> RemovePlanFromFavorites(Guid planId)
    {
        await _favoritePlansRepository.RemoveFromFavorites(Guid.Parse(GetUserId()!), planId);
        //var result = await _elasticService.RemoveAsync(planId.ToString());
        return Ok();
    }
    
    [Authorize]
    [HttpGet("get/all")]
    public async Task<ActionResult<(int, List<PlanModel>)>> GetFavoritePlans([FromQuery] PlanParameters planParameters)
    {
        var response = await _favoritePlansRepository.GetFavorites(Guid.Parse(GetUserId()!), planParameters);
        
        return Ok(new
        {
            totalCount = response.Item1,
            plans = response.Item2
        });
    }
    
    [Authorize]
    [HttpPut("edit/{planId}")]
    public async Task<ActionResult> EditFavoritePlan(Guid planId, [FromBody] PlanRequest request)
    {
        
        var exercises = request.Exercises.Select(e => ExerciseModel.Create(
            Guid.NewGuid(), 
            e.Name,
            e.MuscleGroup
            //false
        ).exerciseModel).ToList();

        var updatedPlanId = await _favoritePlansRepository.EditFavorite(Guid.Parse(GetUserId()!), planId, request.Category, exercises);
        
        var plan = await _favoritePlansRepository.GetFavorite(updatedPlanId);
        
        var result = await _elasticService.AddOrUpdateAsync(plan);
        
        return Ok();
    }
    
}