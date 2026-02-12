using Microsoft.AspNetCore.Mvc;
using Shared.Auth;
using Shared.DTO;
using TrainingPlans.API.DTO;
using TrainingPlans.Domain.Abstractions;
using TrainingPlans.Domain.DTO;

namespace TrainingPlans.API.Controllers;

[ApiController]
[Route("/api/[controller]")]

public class CustomPlansController : ControllerBase
{
    private readonly ICustomPlansService _customPlansService;
    private readonly IUserContextService _userContextService;

    public CustomPlansController(ICustomPlansService customPlansService, IUserContextService userContextService)
    {
        _customPlansService = customPlansService;
        _userContextService = userContextService;
    }
    
    //[Authorize]
    [HttpPost]
    public async Task<ActionResult<Guid>> CreateAsync([FromBody] CustomPlanRequest request, CancellationToken ct)
    {
        var userId = _userContextService.GetUserId();
        var planId = await _customPlansService.CreateCustomPlanAsync(userId, request.Description, 
            request.SourcePlanId, request.Exercises, ct);
        
        return Ok(planId);
    }
    
    [HttpGet]
    public async Task<ActionResult<List<PlanResponse>>> GetAllAsync(CancellationToken ct)
    {
        var userId = _userContextService.GetUserId();
        var plans = await _customPlansService.GetAllCustomPlansAsync(userId, ct);

        return Ok(plans);
    }
    
    [HttpGet("completed")]
    public async Task<ActionResult<List<PlanResponse>>> GetCompletedAsync(CancellationToken ct)
    {
        var userId = _userContextService.GetUserId();
        var plans = await _customPlansService.GetCompletedPlansAsync(userId, ct);
        
        return Ok(plans);
    }
    
    [HttpPut("{planId:guid}")]
    public async Task<ActionResult<Guid>> UpdateAsync(Guid planId, string? description,
        List<CustomPlanExercise>? exercises, CancellationToken ct)
    {
        var userId = _userContextService.GetUserId();
        var id = await _customPlansService.UpdateCustomPlanAsync(userId, planId, description, exercises, ct);
        
        return Ok(id);
    }
    
    [HttpDelete("{planId:guid}")]
    public async Task<ActionResult<Guid>> DeleteAsync(Guid planId, CancellationToken ct)
    {
        var userId = _userContextService.GetUserId();
        var id = await _customPlansService.DeleteCustomPlanAsync(userId, planId, ct);
        
        return Ok(id);
    }
    
    [HttpPatch("{planId:guid}")]
    public async Task<ActionResult<Guid>> UpdateCompletionStatusAsync(
        Guid planId, 
        [FromBody] UpdatePlanCompletionRequst request,
        CancellationToken ct)
    {
        var userId = _userContextService.GetUserId();
    
        var id = request.IsCompleted 
            ? await _customPlansService.CompleteCustomPlanAsync(userId, planId, ct)
            : await _customPlansService.UncompleteCustomPlanAsync(userId, planId, ct);
    
        return Ok(id);
    }
}