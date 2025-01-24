using Microsoft.AspNetCore.Mvc;

using TrainingPlans.Contracts;
using TrainingPlans.Models;
using TrainingPlans.Repositories.Interfaces;
using UserMicroservice.Repositories.Interfaces;

namespace TrainingPlans.Controllers;

[ApiController]
[Route("/api/[controller]")]

public class PlansController : ControllerBase
{
    private readonly IPlansRepository _plansRepository;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IJwtExtractor _jwtExtractor;

    public PlansController(IPlansRepository plansRepository, IHttpContextAccessor httpContextAccessor, IJwtExtractor jwtExtractor)
    {
        _plansRepository = plansRepository;
        _httpContextAccessor = httpContextAccessor;
        _jwtExtractor = jwtExtractor;
    }
    [HttpPost("create")]
    public async Task<ActionResult<Guid>> CreatePlan([FromBody] PlanRequest request)
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
        
        var (plan, response) = PlanModel.Create(Guid.NewGuid(), request.name, exercises, false, userId);
        
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
        var exercises = request.exercises.Select(e => ExerciseModel.Create(
            Guid.NewGuid(), 
            e.name,
            e.muscleGroup,
            null,
            null
        ).exerciseModel).ToList();
        var planId = await _plansRepository.Update(id, request.name, exercises);
        return Ok(planId);
    }
    [HttpDelete("delete/{id}")]
    public async Task<ActionResult<Guid>> DeletePlan(Guid id)
    {
        var planId = await _plansRepository.Delete(id);
        return Ok(planId);
    }
    
}