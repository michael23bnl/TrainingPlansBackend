
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TrainingPlans.Contracts;
using TrainingPlans.Models;
using TrainingPlans.Entities;
using TrainingPlans.Pagination;
using TrainingPlans.Repositories.Interfaces;
using TrainingPlans.Services;


namespace TrainingPlans.Controllers;

[ApiController]
[Route("/api/[controller]")]

public class PlansController : ControllerBase
{
    private readonly IPlansRepository _plansRepository;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IElasticService _elasticService;

    public PlansController(IPlansRepository plansRepository, 
        IHttpContextAccessor httpContextAccessor, 
        IElasticService elasticService)
    {
        _plansRepository = plansRepository;
        _httpContextAccessor = httpContextAccessor;
        _elasticService = elasticService;
    }

    private string? GetUserId()
    {
        var userId = _httpContextAccessor.HttpContext!.Request.Headers["X-User-Id"];
        return userId;
    }
    
    [HttpPost("create-index")]
    public async Task<IActionResult> CreateIndexEls(string indexName)
    {
        await _elasticService.CreateIndexIfNotExistsAsync(indexName);
        return Ok();
    }
    
    /*[HttpPut("update-plan")]
    public async Task<IActionResult> UpdatePlanEls([FromBody] PlanEntity plan)
    {
        var result = await _elasticService.AddOrUpdateAsync(plan);
        return result ? Ok("План успешно обновлён") : 
            StatusCode(500, "Ошибка при обновлении плана");
    }*/
    
    /*[HttpPost("add-plan")]
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
    }*/
    
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
    }
    
    [HttpDelete("delete-all-plans")]
    public async Task<IActionResult> DeleteAllPlansEls()
    {
        var deletedDocuments = await _elasticService.RemoveAll();
        return Ok(deletedDocuments);
    }
    
    [HttpGet("get-plan")]
    public async Task<IActionResult> GetPlanEls(string id)
    {
        var plan = await _elasticService.GetAsync(id);
        return Ok(plan);
    }
    
    [HttpGet("get-all-plans")]
    public async Task<IActionResult> GetAllPlansEls()
    {
        var plans = await _elasticService.GetAllAsync();
        return Ok(plans);
    }
    
    [HttpGet("search/{query}")]
    public async Task<IActionResult> Search(string query, [FromQuery] PlanParameters planParameters)
    {
        if (string.IsNullOrWhiteSpace(query))
        {
            return BadRequest("Search query cannot be empty");
        }

        var results = await _elasticService.SearchPlansAsync(query, planParameters);
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
        
        Guid userId = Guid.Parse(GetUserId());

        var results = await _elasticService.SearchThroughMyPlans(query, userId, planParameters);
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
        
        Guid userId = Guid.Parse(GetUserId());

        var favoritePlanIds = await _plansRepository.GetFavoritePlanIds(userId);

        var results = await _elasticService.SearchThroughFavoritePlans(query, favoritePlanIds, planParameters);
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
        
        Guid userId = Guid.Parse(GetUserId());

        var completedPlanIds = await _plansRepository.GetCompletedPlanIds(userId);

        var results = await _elasticService.SearchThroughCompletedPlans(query, completedPlanIds, planParameters);
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
        var exercises = request.Exercises.Select(e => ExerciseModel.Create(
            Guid.NewGuid(), 
            e.Name,
            e.MuscleGroup
            //false
        ).exerciseModel).ToList();
        
        var (plan, response) = PlanModel.Create(Guid.NewGuid(), request.Category, exercises, Guid.Parse(GetUserId()!));
        
        if (response != "Plan has been created")
        {
            return BadRequest(response);
        }

        var planId = await _plansRepository.Create(plan);

        var planEntity = new PlanEntity
        {
            Id = planId,
            Category = plan.Category,
            Exercises = plan.Exercises.Select(p => new ExerciseEntity
            {
                Id = p.Id,
                Name = p.Name,
                MuscleGroup = p.MuscleGroup
            }).ToList(),
            CreatedBy = plan.CreatedBy,
        };
        
        var result = await _elasticService.AddOrUpdateAsync(planEntity);

        return Ok(planId);
    }
    [Authorize("Create")]
    [HttpPost("create-prepared")]
    public async Task<ActionResult<Guid>> CreatePreparedPlan([FromBody] PlanRequest request)
    {
        
        var exercises = request.Exercises.Select(e => ExerciseModel.Create(
            Guid.NewGuid(), 
            e.Name,
            e.MuscleGroup
            //true
        ).exerciseModel).ToList();
        
        var (plan, response) = PlanModel.Create(Guid.NewGuid(), request.Category, exercises, null);
        
        if (response != "Plan has been created")
        {
            return BadRequest(response);
        }

        var planId = await _plansRepository.Create(plan);

        return Ok(planId);
    }
    
    [Authorize]
    [HttpGet("get/all")]
    public async Task<ActionResult<(int, List<CompletedPlanResponse>)>> GetAllPlans([FromQuery] PlanParameters planParameters)
    {
        var userId = GetUserId();
        var response = await _plansRepository.GetAllSelfMade(Guid.Parse(GetUserId()!), planParameters);
        
        return Ok(new
        {
            totalCount = response.Item1,
            plans = response.Item2
        });
    }
    
    [Authorize]
    [HttpGet("get/all-available")]
    public async Task<ActionResult<List<PlanModel>>> GetAllAvailablePlans()
    {
        return Ok(await _plansRepository.GetAllAvailable(Guid.Parse(GetUserId()!)));
    }
    

    [HttpGet("get/all-prepared")]
    public async Task<ActionResult<(int, List<PlanModel>)>> GetAllPreparedPlans([FromQuery] PlanParameters planParameters) // переработать
    {
        var userId = Guid.TryParse(GetUserId(), out var id) ? id : (Guid?)null;
        var response = await _plansRepository.GetAllPrepared(userId, planParameters);
        
        return Ok(new
        {
            totalCount = response.Item1,
            plans = response.Item2
        });

    }
    
    [Authorize("Create")]
    [HttpGet("get/{id:guid}")]
    public async Task<ActionResult<PreparedPlanResponse>> GetPlan(Guid id)
    {
        var userId = GetUserId();
        var plan = await _plansRepository.Get(id, Guid.Parse(userId));
        if (plan == null)   
        {
            return BadRequest("Plan does not exist");
        }
        
        return Ok(plan);
    }
    
    [Authorize]
    [HttpPut("update/{id}")]
    public async Task<ActionResult<Guid>> UpdatePlan(Guid id, [FromBody] PlanRequest request)
    {
        var exercises = request.Exercises.Select(e => ExerciseModel.Create(
            Guid.NewGuid(), 
            e.Name,
            e.MuscleGroup
        ).exerciseModel).ToList();
        
        var planId = await _plansRepository.Update(id, request.Category, exercises);
        
        var planEntity = new PlanEntity
        {
            Id = planId,
            Category = request.Category,
            Exercises = exercises.Select(p => new ExerciseEntity
            {
                Id = p.Id,
                Name = p.Name,
                MuscleGroup = p.MuscleGroup
            }).ToList(),
            CreatedBy = Guid.Parse(GetUserId()!),
        };
        
        var result = await _elasticService.AddOrUpdateAsync(planEntity);

        return Ok(planId);
    }
    
    [Authorize]
    [HttpDelete("delete/{id}")]
    public async Task<ActionResult<Guid>> DeletePlan(Guid id)
    {
        var planId = await _plansRepository.Delete(id);
        var result = await _elasticService.RemoveAsync(id.ToString());
        return Ok(planId);
    }
    
}