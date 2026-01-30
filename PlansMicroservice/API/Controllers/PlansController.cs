
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Auth;
using Shared.DTO;
using TrainingPlans.API.DTO;
using Shared.Pagination;
using TrainingPlans.Domain.Abstractions;

namespace TrainingPlans.API.Controllers;

[ApiController]
[Route("/api/[controller]")]

public class PlansController : ControllerBase
{

    private readonly IPlansService _plansService;
    private readonly IUserContextService _userContextService;

    public PlansController(IPlansService plansService, IUserContextService userContextService)
    {
        _plansService = plansService;
        _userContextService = userContextService;
    }
    
    [Authorize]
    [HttpPost("create")]
    public async Task<ActionResult<Guid>> CreatePlanAsync([FromBody] PlanRequest request, CancellationToken ct)
    {
        var userId = _userContextService.GetUserId();
        var planId = await _plansService.CreatePlanAsync(request.ExerciseIds, userId, ct);

        return Ok(planId);
    }
    
    [Authorize("Create")]
    [HttpPost("create-preloaded")]
    public async Task<ActionResult<Guid>> CreatePreloadedPlanAsync([FromBody] PlanRequest request, CancellationToken ct)
    {
        var planId = await _plansService.CreatePlanAsync(request.ExerciseIds, null, ct);

        return Ok(planId);
    }
    
    [HttpGet("get/all")]
    public async Task<ActionResult> GetAllPreloadedPlansAsync([FromQuery] PlanParameters planParameters, CancellationToken ct)
    {

        var response = await _plansService.GetAllPreloadedPlansAsync(planParameters, ct);
        
        return Ok(new
        {
            totalCount = response.Item1,
            plans = response.Item2
        });
    }
    
    [Authorize]
    [HttpGet("get")]
    public async Task<ActionResult<PlanResponse>> GetPlanAsync(Guid planId, CancellationToken ct)
    {
        var userId = _userContextService.GetUserId();
        var response = await _plansService.GetPlanAsync(planId, userId, ct);

        return Ok(response);
    }
    
    [Authorize]
    [HttpPut("update/{id}")]
    public async Task<ActionResult<Guid>> UpdatePlan(Guid id, [FromBody] PlanRequest request, CancellationToken ct)
    {
        var planId = await _plansService.UpdatePlanAsync(id, request.ExerciseIds, ct);
        
        return Ok(planId);
    }
    
    [Authorize]
    [HttpDelete("delete/{id}")]
    public async Task<ActionResult<Guid>> DeletePlan(Guid id, CancellationToken ct)
    {
        var planId = await _plansService.DeletePlanAsync(id, ct);
        
        return Ok(planId);
    }
    
}