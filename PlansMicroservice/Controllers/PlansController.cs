using Microsoft.AspNetCore.Mvc;

using TrainingPlans.Contracts;
using TrainingPlans.Models;
using TrainingPlans.Entities;
using TrainingPlans.Repositories.Interfaces;
using TrainingPlans.Services;
using UserMicroservice.Repositories.Interfaces;

namespace TrainingPlans.Controllers;

[ApiController]
[Route("/api/[controller]")]

public class PlansController : ControllerBase
{
    private readonly IPlansRepository _plansRepository;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IJwtExtractor _jwtExtractor;
    private readonly IElasticService _elasticService;

    public PlansController(IPlansRepository plansRepository, 
        IHttpContextAccessor httpContextAccessor, 
        IJwtExtractor jwtExtractor,
        IElasticService elasticService)
    {
        _plansRepository = plansRepository;
        _httpContextAccessor = httpContextAccessor;
        _jwtExtractor = jwtExtractor;
        _elasticService = elasticService;
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
                    MuscleGroup = e.MuscleGroup,
                    IsPreMade = e.IsPreMade
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
    
    [Permission("Read")]
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
    
    [HttpPost("create")]
    public async Task<ActionResult<Guid>> CreatePlan([FromBody] PlanRequest request)
    {
     
        var token = _httpContextAccessor.HttpContext?.Request.Cookies["suchatastycookie"];

        var userId = Guid.Parse(_jwtExtractor.ExtractUserIdFromJwtToken(token));
        
        var exercises = request.Exercises.Select(e => ExerciseModel.Create(
            Guid.NewGuid(), 
            e.Name,
            e.MuscleGroup,
            false
        ).exerciseModel).ToList();
        
        var (plan, response) = PlanModel.Create(Guid.NewGuid(), request.Category, exercises, userId);
        
        if (response != "Plan has been created")
        {
            return BadRequest(response);
        }

        var planId = await _plansRepository.Create(plan);

        return Ok(planId);
    }
    
    [HttpPost("create-prepared")]
    public async Task<ActionResult<Guid>> CreatePreparedPlan([FromBody] PlanRequest request)
    {
        
        var exercises = request.Exercises.Select(e => ExerciseModel.Create(
            Guid.NewGuid(), 
            e.Name,
            e.MuscleGroup,
            true
        ).exerciseModel).ToList();
        
        var (plan, response) = PlanModel.Create(Guid.NewGuid(), request.Category, exercises, null);
        
        if (response != "Plan has been created")
        {
            return BadRequest(response);
        }

        var planId = await _plansRepository.Create(plan);

        return Ok(planId);
    }
    
    [Permission("Read")]
    [HttpGet("get/all")]
    public async Task<ActionResult<List<PlanModel>>> GetAllPlans()
    {
        var token = _httpContextAccessor.HttpContext?.Request.Cookies["suchatastycookie"];

        var userId = Guid.Parse(_jwtExtractor.ExtractUserIdFromJwtToken(token));
        
        return Ok(await _plansRepository.GetAllSelfMade(userId));
    }
    
    [Permission("Read")]
    [HttpGet("get/all-prepared")]
    public async Task<ActionResult<List<PlanModel>>> GetAllPreparedPlans() // переработать
    {
        var token = _httpContextAccessor.HttpContext?.Request.Cookies["suchatastycookie"];
        
        if (token != null)
        {
            var userId = Guid.Parse(_jwtExtractor.ExtractUserIdFromJwtToken(token));
            return Ok(await _plansRepository.GetAllPrepared(userId));

        }
        
        return Ok(await _plansRepository.GetAllPrepared(null));

    }
    
    [Permission("Create")]
    [HttpGet("get/{id:guid}")]
    public async Task<ActionResult<PlanModel>> GetPlan(Guid id)
    {
        var plan = await _plansRepository.Get(id);
        if (plan == null)   
        {
            return BadRequest("Plan does not exist");
            
        }
        
        return Ok(plan);
    }

    [HttpGet("get/{name}")]
    public async Task<ActionResult<PlanModel>> GetPlanByName(string name)
    {
        
        var token = _httpContextAccessor.HttpContext?.Request.Cookies["suchatastycookie"];

        var userId = Guid.Parse(_jwtExtractor.ExtractUserIdFromJwtToken(token));
        
        var plan = await _plansRepository.GetByName(userId, name);
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
            e.MuscleGroup,
            false
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