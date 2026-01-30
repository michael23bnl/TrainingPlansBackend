using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Auth;
using TrainingPlans.Application.Abstractions;

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
    
    [Authorize]
    [HttpPost("add/{planId}")]
    public async Task<ActionResult<Guid>> AddOrRemoveCustomPlanAsync(Guid planId, CancellationToken ct)
    {
        var userId = _userContextService.GetUserId();
        var id = await _customPlansService.AddOrRemoveCustomPlanAsync(userId, planId, ct);
        
        return Ok(id);
    }
    
    [Authorize]
    [HttpDelete("remove/{planId}")]
    public async Task<ActionResult> CompleteCustomPlanAsync(Guid planId, CancellationToken ct)
    {
        var userId = _userContextService.GetUserId();
        var id = await _customPlansService.CompleteCustomPlanAsync(userId, planId, ct);
        
        return Ok(id);
    }
}