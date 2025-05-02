using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using TrainingPlans.Contracts;
using TrainingPlans.Models;
using TrainingPlans.Entities;
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
    
    [HttpDelete("delete-plan")]
    public async Task<IActionResult> DeletePlanEls(string id)
    {
        var result = await _elasticService.RemoveAsync(id);
        return Ok(result);
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
    
    //[Permission("Read")]
    [HttpGet("search/{query}")]
    public async Task<IActionResult> Search(string query)
    {
        if (string.IsNullOrWhiteSpace(query))
        {
            return BadRequest("Search query cannot be empty");
        }

        var results = await _elasticService.SearchPlansAsync(query);
        return Ok(results);
    }
    
    [HttpGet("search/my-plans/{query}")]
    public async Task<IActionResult> SearchThroughMyPlans(string query)
    {
        if (string.IsNullOrWhiteSpace(query))
        {
            return BadRequest("Search query cannot be empty");
        }
        
        Guid userId = Guid.Parse(GetUserId());

        var results = await _elasticService.SearchThroughMyPlans(query, userId);
        return Ok(results);
    }
    
    /*[HttpGet("search/favorites/{query}")]
    public async Task<IActionResult> SearchThroughFavorites(string query)
    {
        if (string.IsNullOrWhiteSpace(query))
        {
            return BadRequest("Search query cannot be empty");
        }
        
        Guid userId = Guid.Parse(GetUserId());
        
        var favoritePlanIds = await _fa

        var results = await _elasticService.SearchThroughFavoritePlans(query);
        return Ok(results);
    }*/
    
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
        return result ? Ok("План успешно добавлен") : 
            StatusCode(500, "Ошибка при добавлении плана");

        return Ok(planId);
    }
    
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
    
    [Permission("Create")]
    [HttpGet("get/all")]
    public async Task<ActionResult<List<PlanModel>>> GetAllPlans()
    {
        var userId = GetUserId();
        return Ok(await _plansRepository.GetAllSelfMade(Guid.Parse(GetUserId()!)));
    }
    [Permission("Create")]
    [HttpGet("get/all-available")]
    public async Task<ActionResult<List<PlanModel>>> GetAllAvailablePlans()
    {
        return Ok(await _plansRepository.GetAllAvailable(Guid.Parse(GetUserId()!)));
    }
    
    //[Permission("Read")]
    [HttpGet("get/all-prepared")]
    public async Task<ActionResult<List<PlanModel>>> GetAllPreparedPlans() // переработать
    {
        Guid? userId = Guid.Parse(GetUserId());
        
        return Ok(await _plansRepository.GetAllPrepared(userId));

    }
    
    //[Permission("Create")]
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

    [HttpGet("get/{name}")]
    public async Task<ActionResult<PlanModel>> GetPlanByName(string name)
    {
        var plan = await _plansRepository.GetByName(Guid.Parse(GetUserId()), name);
        if (plan == null)
        {
            return BadRequest($"Plan with name {name} does not exist");
        }

        return plan;
    }
    
    [HttpPut("update/{id}")]
    public async Task<ActionResult<Guid>> UpdatePlan(Guid id, [FromBody] PlanRequest request)
    {
        var exercises = request.Exercises.Select(e => ExerciseModel.Create(
            Guid.NewGuid(), 
            e.Name,
            e.MuscleGroup
            //false
        ).exerciseModel).ToList();
        var planId = await _plansRepository.Update(id, request.Category, exercises);
        return Ok(planId);
    }
    
    [HttpDelete("delete/{id}")]
    public async Task<ActionResult<Guid>> DeletePlan(Guid id)
    {
        var planId = await _plansRepository.Delete(id);
        return Ok(planId);
    }
    
}