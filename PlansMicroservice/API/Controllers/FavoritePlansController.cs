using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TrainingPlans.API.DTO;
using TrainingPlans.Application.Services.Interfaces;
using TrainingPlans.Domain.Models;
using TrainingPlans.Pagination;

namespace TrainingPlans.API.Controllers;

[ApiController]
[Route("/api/[controller]")]

public class FavoritePlansController : ControllerBase
{
    private readonly IFavoritePlansService _favoritePlansService;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IPlansSearch _plansSearch;

    public FavoritePlansController(IFavoritePlansService favoritePlansService, 
        IHttpContextAccessor httpContextAccessor,
        IPlansSearch plansSearch)
    {
        _favoritePlansService = favoritePlansService;
        _httpContextAccessor = httpContextAccessor;
        _plansSearch = plansSearch;
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
        var userId = Guid.Parse(GetUserId()!);
        await _favoritePlansService.AddPlanToFavorites(userId, planId);
        return Ok();
    }
    
    [Authorize]
    [HttpDelete("remove/{planId}")]
    public async Task<ActionResult> RemovePlanFromFavorites(Guid planId)
    {
        var userId = Guid.Parse(GetUserId()!);
        await _favoritePlansService.RemovePlanFromFavorites(userId, planId);
        return Ok();
    }
    
    [Authorize]
    [HttpGet("get/all")]
    public async Task<ActionResult<(int, List<PlanModel>)>> GetFavoritePlans([FromQuery] PlanParameters planParameters)
    {
        var userId = Guid.Parse(GetUserId()!);
        var response = await _favoritePlansService.GetFavoritePlans(userId, planParameters);
        
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
        var userId = Guid.Parse(GetUserId()!);
        
        await _favoritePlansService.EditFavoritePlan(planId, request, userId);
        
        return Ok();
    }
    
}