using Microsoft.AspNetCore.Mvc;
using TrainingPlans.Contracts;
using TrainingPlans.Models;
using TrainingPlans.Repositories.Interfaces;

namespace TrainingPlans.Controllers;

[ApiController]
[Route("/api/[controller]")]

public class PlansController : ControllerBase
{
    private readonly IPlansRepository _plansRepository;

    public PlansController(IPlansRepository plansRepository)
    {
        _plansRepository = plansRepository;
    }
    [HttpPost("create")]
    public async Task<ActionResult<Guid>> CreatePlan([FromBody] PlanRequest request)
    {
        
        var exercises = request.exercises.Select(e => ExerciseModel.Create(
            Guid.NewGuid(), 
            e.name,
            e.muscleGroup
        ).exerciseModel).ToList();
        
        var (plan, response) = PlanModel.Create(Guid.NewGuid(), exercises);
        
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
        return Ok(await _plansRepository.GetAll());
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
    [HttpPut("update/{id}")]
    public async Task<ActionResult<Guid>> UpdatePlan(Guid id, [FromBody] PlanRequest request)
    {
        var exercises = request.exercises.Select(e => ExerciseModel.Create(
            Guid.NewGuid(), 
            e.name,
            e.muscleGroup
        ).exerciseModel).ToList();
        var planId = await _plansRepository.Update(id, exercises);
        return Ok(planId);
    }
    [HttpDelete("delete/{id}")]
    public async Task<ActionResult<Guid>> DeletePlan(Guid id)
    {
        var planId = await _plansRepository.Delete(id);
        return Ok(planId);
    }
    
}