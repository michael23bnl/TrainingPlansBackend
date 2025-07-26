
using System.Text.Json;
using Dapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Npgsql;
using TrainingPlans.API.DTO;
using TrainingPlans.Application.Services.Interfaces;
using TrainingPlans.Domain.Models;
using TrainingPlans.Pagination;


namespace TrainingPlans.API.Controllers;

[ApiController]
[Route("/api/[controller]")]

public class PlansController : ControllerBase
{

    private readonly IPlansService _plansService;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IPlansSearch _plansSearch;

    public PlansController(IPlansService plansService, 
        IHttpContextAccessor httpContextAccessor, 
        IPlansSearch plansSearch)
    {
        _plansService = plansService;
        _httpContextAccessor = httpContextAccessor;
        _plansSearch = plansSearch;
    }

    private string? GetUserId()
    {
        var userId = _httpContextAccessor.HttpContext!.Request.Headers["X-User-Id"];
        return userId;
    }
    
    /*[HttpPost("create-index")]
    public async Task<IActionResult> CreateIndexEls(string indexName)
    {
        await _elasticService.CreateIndexIfNotExistsAsync(indexName);
        return Ok();
    }
    
    [HttpPut("update-plan")]
    public async Task<IActionResult> UpdatePlanEls([FromBody] PlanEntity plan)
    {
        var result = await _elasticService.AddOrUpdateAsync(plan);
        return result ? Ok("План успешно обновлён") : 
            StatusCode(500, "Ошибка при обновлении плана");
    }
    
    [HttpPost("add-plan")]
    public async Task<IActionResult> AddPlanEls([FromBody] PlanEntity plan)
    {
        var result = await _elasticService.AddOrUpdateAsync(plan);
        return result ? Ok("План успешно добавлен") : 
            StatusCode(500, "Ошибка при добавлении плана");
    }
    
    [HttpDelete("delete-plan")]
    public async Task<IActionResult> DeletePlanEls(string id)
    {
        var result = await _elasticService.RemoveAsync(id);
        return Ok(result);
    }
    
    [HttpPost("add-all-plans")]
    public async Task<IActionResult> AddAllPlansEls()
    {
        var planResponses = await _plansRepository.GetAllPrepared();
        var planEntities = planResponses.Select(p => 
            new PlanEntity
            {
                Id = p.Id,
                Category = p.Category,
                Exercises = p.Exercises.Select(e => new ExerciseEntity
                {
                    Id = e.Id,
                    Name = e.Name,
                    MuscleGroup = e.MuscleGroup
                    //IsPreMade = e.IsPreMade
                }).ToList(),
                CreatedBy = p.CreatedBy
            }).ToList();
        await _elasticService.AddOrUpdateBulk(planEntities);
        return Ok();
    }*/
    
    [HttpDelete("delete-all-plans")]
    public async Task<IActionResult> DeleteAllPlansEls()
    {
        var deletedDocuments = await _plansSearch.RemoveAll();
        return Ok(deletedDocuments);
    }
    
    [HttpGet("get-plan")]
    public async Task<IActionResult> GetPlanEls(string id)
    {
        var plan = await _plansSearch.GetAsync(id);
        return Ok(plan);
    }
    
    [HttpGet("get-all-plans")]
    public async Task<IActionResult> GetAllPlansEls()
    {
        var plans = await _plansSearch.GetAllAsync();
        return Ok(plans);
    }

    [HttpGet("search/{query}")]
    public async Task<IActionResult> Search(string query, [FromQuery] PlanParameters planParameters)
    {

        if (string.IsNullOrWhiteSpace(query))
        {
            return BadRequest("Search query cannot be empty");
        }

        // var results = await _plansSearch.SearchPlansAsync(query, planParameters);
        
        var results = await _plansService.SearchCatalogPlans(query, planParameters);

        return Ok(new
        {
            totalCount = results.totalCount,
            plans = results.plans
        });
    }

    [Authorize]
    [HttpGet("search/my-plans/{query}")]
    public async Task<IActionResult> SearchThroughMyPlans(string query, [FromQuery] PlanParameters planParameters)
    {
        if (string.IsNullOrWhiteSpace(query))
        {
            return BadRequest("Search query cannot be empty");
        }
        
        var userId = Guid.Parse(GetUserId()!);

        // var results = await _plansSearch.SearchThroughMyPlans(query, userId, planParameters);
        
        var results = await _plansService.SearchMyPlans(query, planParameters, userId);
        
        return Ok(new
        {
            totalCount = results.totalCount,
            plans = results.plans
        });
    }
    [Authorize]
    [HttpGet("search/favorites/{query}")]
    public async Task<IActionResult> SearchThroughFavorites(string query, [FromQuery] PlanParameters planParameters)
    {
        if (string.IsNullOrWhiteSpace(query))
        {
            return BadRequest("Search query cannot be empty");
        }
        
        var userId = Guid.Parse(GetUserId()!);

        // var favoritePlanIds = await _plansService.GetFavoritePlansIds(userId);

        // var results = await _plansSearch.SearchThroughFavoritePlans(query, favoritePlanIds, planParameters);
        
        var results = await _plansService.SearchFavoritePlans(query, planParameters, userId);
        
        return Ok(new
        {
            totalCount = results.totalCount,
            plans = results.plans
        });
    }
    [Authorize]
    [HttpGet("search/completed-plans/{query}")]
    public async Task<IActionResult> SearchThroughCompletedPlans(string query, [FromQuery] PlanParameters planParameters)
    {
        if (string.IsNullOrWhiteSpace(query))
        {
            return BadRequest("Search query cannot be empty");
        }
        
        var userId = Guid.Parse(GetUserId()!);

        // var completedPlanIds = await _plansService.GetCompletedPlansIds(userId);
        
        // var results = await _plansSearch.SearchThroughCompletedPlans(query, completedPlanIds, planParameters);
        
        var results = await _plansService.SearchCompletedPlans(query, planParameters, userId);
        
        return Ok(new
        {
            totalCount = results.totalCount,
            plans = results.plans
        });
    }
    
    [Authorize]
    [HttpPost("create")]
    public async Task<ActionResult<Guid>> CreatePlan([FromBody] PlanRequest request)
    {
        var userId = Guid.Parse(GetUserId());
        var planId = await _plansService.CreatePlan(request, userId);

        if (planId == Guid.Empty)
        {
            return BadRequest("Не удалось создать план");
        }

        return Ok(planId);
    }
    
    [Authorize("Create")]
    [HttpPost("create-prepared")]
    public async Task<ActionResult<Guid>> CreatePreparedPlan([FromBody] PlanRequest request)
    {
        var planId = await _plansService.CreatePlan(request, null);

        if (planId == Guid.Empty)
        {
            return BadRequest("Не удалось создать план");
        }

        return Ok(planId);
    }
    
    [Authorize]
    [HttpGet("get/all")]
    public async Task<ActionResult<(int, List<CompletedPlanResponse>)>> GetAllSelfMadePlans([FromQuery] PlanParameters planParameters)
    {
        var userId = Guid.Parse(GetUserId());
        var response = await _plansService.GetAllSelfMadePlans(planParameters, userId);
        
        return Ok(new
        {
            totalCount = response.Item1,
            plans = response.Item2
        });
    }
    
    [Authorize]
    [HttpGet("get/all-available")]
    public async Task<ActionResult<List<PreparedPlanResponse>>> GetAllAvailablePlans()
    {
        var userId = Guid.Parse(GetUserId());
        var response = await _plansService.GetAllAvailablePlans(userId);
        return response;
    }
    

    [HttpGet("get/all-prepared")]
    public async Task<ActionResult<(int, List<PlanModel>)>> GetAllPreparedPlans([FromQuery] PlanParameters planParameters)
    {
        var userId = Guid.TryParse(GetUserId(), out var id) ? id : (Guid?)null;
        var response = await _plansService.GetAllPreparedPlans(planParameters, userId);
        
        return Ok(new
        {
            totalCount = response.Item1,
            plans = response.Item2
        });

    }
    
    [Authorize]
    [HttpGet("get/{id:guid}")]
    public async Task<ActionResult<PreparedPlanResponse>> GetPlan(Guid id)
    {
        var userId = Guid.Parse(GetUserId()!);
        var plan = await _plansService.GetPlan(id, userId);
        
        return Ok(plan);
    }
    
    [Authorize]
    [HttpPut("update/{id}")]
    public async Task<ActionResult<Guid>> UpdatePlan(Guid id, [FromBody] PlanRequest request)
    {
        var userId = Guid.Parse(GetUserId()!);
        var planId = await _plansService.UpdatePlan(id, request, userId);
        
        return Ok(planId);
    }
    
    [Authorize]
    [HttpDelete("delete/{id}")]
    public async Task<ActionResult<Guid>> DeletePlan(Guid id)
    {
        var planId = await _plansService.DeletePlan(id);
        
        return Ok(planId);
    }
    
}