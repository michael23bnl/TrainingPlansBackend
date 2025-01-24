using Microsoft.AspNetCore.Mvc;

using TrainingPlans.Contracts;
using TrainingPlans.Models;
using TrainingPlans.Repositories.Interfaces;
using UserMicroservice.Repositories.Interfaces;

namespace TrainingPlans.Controllers;

[ApiController]
[Route("/api/[controller]")]

public class PreparedPlansController : ControllerBase
{
    private readonly IPlansRepository _plansRepository;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IJwtExtractor _jwtExtractor;


    public PreparedPlansController(IPlansRepository plansRepository, IHttpContextAccessor httpContextAccessor, IJwtExtractor jwtExtractor)
    {
        _plansRepository = plansRepository;
        _httpContextAccessor = httpContextAccessor;
        _jwtExtractor = jwtExtractor;
    }
    
    [HttpPost("create")]
    public async Task<ActionResult<Guid>> CreatePreparedPlan([FromBody] PlanRequest request)
    {
        
        var exercises = request.exercises.Select(e => ExerciseModel.Create(
            Guid.NewGuid(), 
            e.name,
            e.muscleGroup,
            null,
            null
        ).exerciseModel).ToList();
        
        var (plan, response) = PlanModel.Create(Guid.NewGuid(), request.name, exercises, true, null);
        
        if (response != "Plan has been created")
        {
            return BadRequest(response);
        }

        var planId = await _plansRepository.Create(plan);

        return Ok(planId);
    }
    
    
    /*[HttpGet("get/all")]
    public async Task<ActionResult<List<PlanModel>>> GetAllPlans()
    {
        return Ok(await _plansRepository.GetAllPrepared());
    }*/
    
    [HttpGet("get/all")]
    public async Task<ActionResult<List<PlanModel>>> GetAllPlans() // переработать
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
    [HttpGet("get/{id}")]
    public async Task<ActionResult<PlanModel>> GetPlan(Guid id)
    {
        var plan = await _plansRepository.Get(id);
        if (plan != null)
        {
            return Ok(plan);
        }

        return BadRequest("Plan does not exist");
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