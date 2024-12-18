using Microsoft.AspNetCore.Mvc;
using TrainingPlans.Repositories.Interfaces;
using TrainingPlans.Models;
namespace TrainingPlans.Controllers;

[ApiController]
[Route("/api/[controller]")]

public class FavoritePlansController : ControllerBase
{
    private readonly IFavoritePlansRepository _favoritePlansRepository;

    public FavoritePlansController(IFavoritePlansRepository favoritePlansRepository)
    {
        _favoritePlansRepository = favoritePlansRepository;
    }
    [HttpPost("add-to-favorites")]
    public async Task<ActionResult> AddPlanToFavorites(Guid userId, Guid planId)
    {
        _favoritePlansRepository.AddToFavorites(userId, planId);
        return Ok();
    }
    [HttpDelete("remove-from-favorites")]
    public async Task<ActionResult> RemovePlanFromFavorites(Guid userId, Guid planId)
    {
        _favoritePlansRepository.RemoveFromFavorites(userId, planId);
        return Ok();
    }
    [HttpGet("get-favorites")]
    public async Task<ActionResult<List<PlanModel>>> GetFavoritePlans(Guid userId)
    {
        return Ok(await _favoritePlansRepository.GetFavorites(userId));
    }
    
}