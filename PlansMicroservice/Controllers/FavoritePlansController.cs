using Microsoft.AspNetCore.Mvc;
using TrainingPlans.Contracts;
using TrainingPlans.Repositories.Interfaces;
using TrainingPlans.Models;
using UserMicroservice.Repositories.Interfaces;

namespace TrainingPlans.Controllers;

[ApiController]
[Route("/api/[controller]")]

public class FavoritePlansController : ControllerBase
{
    private readonly IFavoritePlansRepository _favoritePlansRepository;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IJwtExtractor _jwtExtractor;

    public FavoritePlansController(IFavoritePlansRepository favoritePlansRepository, IHttpContextAccessor httpContextAccessor, IJwtExtractor jwtExtractor)
    {
        _favoritePlansRepository = favoritePlansRepository;
        _httpContextAccessor = httpContextAccessor;
        _jwtExtractor = jwtExtractor;
    }
    [HttpPost("add/{planId}")]
    public async Task<ActionResult> AddPlanToFavorites(Guid planId)
    {
        
        var token = _httpContextAccessor.HttpContext?.Request.Cookies["suchatastycookie"];

        var userId = Guid.Parse(_jwtExtractor.ExtractUserIdFromJwtToken(token));
        
        await _favoritePlansRepository.AddToFavorites(userId, planId);
        return Ok();
    }
    [HttpDelete("remove/{planId}")]
    public async Task<ActionResult> RemovePlanFromFavorites(Guid planId)
    {
        
        var token = _httpContextAccessor.HttpContext?.Request.Cookies["suchatastycookie"];

        var userId = Guid.Parse(_jwtExtractor.ExtractUserIdFromJwtToken(token));
        
        await _favoritePlansRepository.RemoveFromFavorites(userId, planId);
        return Ok();
    }
    [HttpGet("get/all")]
    public async Task<ActionResult<List<PlanModel>>> GetFavoritePlans()
    {
        
        var token = _httpContextAccessor.HttpContext?.Request.Cookies["suchatastycookie"];

        var userId = Guid.Parse(_jwtExtractor.ExtractUserIdFromJwtToken(token));
        
        return Ok(await _favoritePlansRepository.GetFavorites(userId));
    }
    
    [HttpPut("edit/{planId}")]
    public async Task<ActionResult> EditFavoritePlan(Guid planId, [FromBody] PlanRequest request)
    {
        
        var token = _httpContextAccessor.HttpContext?.Request.Cookies["suchatastycookie"];

        var userId = Guid.Parse(_jwtExtractor.ExtractUserIdFromJwtToken(token));
        
        var exercises = request.exercises.Select(e => ExerciseModel.Create(
            Guid.NewGuid(), 
            e.name,
            e.muscleGroup,
            null,
            null
        ).exerciseModel).ToList();

        await _favoritePlansRepository.EditFavorite(userId, planId, request.name, exercises);
        
        return Ok();
    }
    
}