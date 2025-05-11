using Microsoft.AspNetCore.Mvc;
using TrainingPlans.Models;
using TrainingPlans.Pagination;
using TrainingPlans.Repositories.Interfaces;

namespace TrainingPlans.Controllers;
[ApiController]
[Route("/api/[controller]")]

public class CompletedPlansController : ControllerBase
{
    private readonly ICompletedPlansRepository _completedPlansRepository;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CompletedPlansController(ICompletedPlansRepository completedPlansRepository, 
        IHttpContextAccessor httpContextAccessor)
    {
        _completedPlansRepository = completedPlansRepository;
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
        await _completedPlansRepository.MarkAsCompleted(Guid.Parse(GetUserId()!), planId);
        return Ok();
    }
    [HttpDelete("unmark/{planId}")]
    public async Task<ActionResult> RemovePlanCompletedMark(Guid planId)
    {
        await _completedPlansRepository.RemoveCompletedMark(Guid.Parse(GetUserId()!), planId);
        return Ok();
    }
    [HttpGet("get/all")]
    public async Task<ActionResult<List<CompletedPlanModel>>> GetCompletedPlans([FromQuery] PlanParameters planParameters)
    {
        return Ok(await _completedPlansRepository.GetCompletedPlans(Guid.Parse(GetUserId()!)));
    }
    
    [HttpGet("get/all/paginated")]
    public async Task<ActionResult<(int, List<PlanModel?>)>> GetCompletedPlansPaginated([FromQuery] PlanParameters planParameters)
    {
        var response = await _completedPlansRepository.GetCompletedPlansPaginated(Guid.Parse(GetUserId()!), planParameters);
        
        return Ok(new
        {
            totalCount = response.Item1,
            plans = response.Item2
        });
    }
}