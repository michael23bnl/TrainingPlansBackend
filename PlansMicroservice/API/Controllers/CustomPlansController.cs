using Microsoft.AspNetCore.Mvc;
using Shared.Auth;
using Shared.DTO;
using Shared.Pagination;
using TrainingPlans.API.DTO;
using TrainingPlans.Domain.Abstractions;
using TrainingPlans.Domain.DTO;

namespace TrainingPlans.API.Controllers;

[ApiController]
[Route("/api/[controller]")]

public class CustomPlansController : ControllerBase
{
    private readonly ICustomPlansService _customPlansService;
    private readonly IPlansSearchService _plansSearchService;
    private readonly IUserContextService _userContextService;

    public CustomPlansController(ICustomPlansService customPlansService, IPlansSearchService plansSearchService, 
        IUserContextService userContextService)
    {
        _customPlansService = customPlansService;
        _plansSearchService = plansSearchService;
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
    
    [HttpGet("search/{query}")]
    public async Task<ActionResult<PlanResponse>> SearchAsync(string query, [FromQuery] PlanParameters planParameters,
        CancellationToken ct)
    {
        var userId = _userContextService.GetUserId();
        var response = await _plansSearchService.SearchCustomPlansAsync(query, userId, planParameters, ct);

        return Ok(new
        {
            totalCount = response.Item1,
            plans = response.Item2
        });
    }
    
    [HttpGet("{planId:guid}")]
    public async Task<ActionResult<PlanResponse>> GetAsync(Guid planId, CancellationToken ct)
    {
        var userId = _userContextService.GetUserId();
        var response = await _customPlansService.GetPlanAsync(userId, planId, ct);

        return Ok(response);
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