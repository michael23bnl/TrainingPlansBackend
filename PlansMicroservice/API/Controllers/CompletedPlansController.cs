using Microsoft.AspNetCore.Mvc;
using TrainingPlans.Application.Services;
using TrainingPlans.Models;
using TrainingPlans.Pagination;
using TrainingPlans.Repositories.Interfaces;

namespace TrainingPlans.Controllers;
[ApiController]
[Route("/api/[controller]")]

public class CompletedPlansController : ControllerBase
{
    private readonly ICompletedPlansService _completedPlansService;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CompletedPlansController(ICompletedPlansService completedPlansService, 
        IHttpContextAccessor httpContextAccessor)
    {
        _completedPlansService = completedPlansService;
        _httpContextAccessor = httpContextAccessor;
    }
    
    private string? GetUserId()
    {
        var userId = _httpContextAccessor.HttpContext!.Request.Headers["X-User-Id"];
        return userId;
    }
    
    [HttpPost("mark/{planId}")]
    public async Task<ActionResult> MarkPlanAsCompleted(Guid planId)
    {
        await _completedPlansService.MarkAsCompleted(Guid.Parse(GetUserId()!), planId);
        return Ok();
    }
    [HttpDelete("unmark/{planId}")]
    public async Task<ActionResult> RemovePlanCompletedMark(Guid planId)
    {
        await _completedPlansService.RemoveCompletedMark(Guid.Parse(GetUserId()!), planId);
        return Ok();
    }
    [HttpGet("get/all")]
    public async Task<ActionResult<List<CompletedPlanModel>>> GetCompletedPlans([FromQuery] PlanParameters planParameters)
    {
        return Ok(await _completedPlansService.GetCompletedPlans(Guid.Parse(GetUserId()!)));
    }
    
    [HttpGet("get/all/paginated")]
    public async Task<ActionResult<(int, List<PlanModel?>)>> GetCompletedPlansPaginated([FromQuery] PlanParameters planParameters)
    {
        var response = await _completedPlansService.GetCompletedPlans(Guid.Parse(GetUserId()!), planParameters);
        
        return Ok(new
        {
            totalCount = response.Item1,
            plans = response.Item2
        });
    }
}