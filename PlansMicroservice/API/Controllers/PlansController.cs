
using Microsoft.AspNetCore.Mvc;
using Shared.DTO;
using TrainingPlans.API.DTO;
using Shared.Pagination;
using TrainingPlans.Application.Abstractions;
using TrainingPlans.Domain.Abstractions;

namespace TrainingPlans.API.Controllers;

[ApiController]
[Route("/api/[controller]")]

public class PlansController : ControllerBase
{
    private readonly IPlansService _plansService;
    private readonly IPlansSearchService _plansSearchService;

    public PlansController(IPlansService plansService, IPlansSearchService plansSearchService)
    {
        _plansService = plansService;
        _plansSearchService = plansSearchService;
    }
    
    //[Authorize]
    [HttpPost]
    public async Task<ActionResult<Guid>> CreateAsync([FromBody] PlanRequest request, CancellationToken ct)
    {
        var planId = await _plansService.CreatePlanAsync(request.Exercises, request.Description, ct);

        return Ok(planId);
    }
    
    [HttpGet]
    public async Task<ActionResult> GetAllAsync([FromQuery] PlanParameters planParameters, 
        CancellationToken ct)
    {
        var response = await _plansService.GetAllPlansAsync(planParameters, ct);
        
        return Ok(new
        {
            totalCount = response.Item1,
            plans = response.Item2
        });
    }
    
    //[Authorize]
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<PlanResponse>> GetAsync(Guid id, CancellationToken ct)
    {
        var response = await _plansService.GetPlanAsync(id, ct);

        return Ok(response);
    }

    [HttpGet("search/{query}")]
    public async Task<ActionResult<PlanResponse>> SearchAsync(string query, [FromQuery] PlanParameters planParameters,
        CancellationToken ct)
    {
        var response = await _plansSearchService.SearchPlansAsync(query, planParameters, ct);

        return Ok(new
        {
            totalCount = response.Item1,
            plans = response.Item2
        });
    }

    [HttpPost("batch")]
    public async Task<ActionResult<List<PlanResponse>>> GetByIdsAsync(
        [FromBody] GetPlansByIdsRequest request,
        CancellationToken ct)
    {
        var response = await _plansService.GetPlansByIdsAsync(request.Ids, ct);
        
        return Ok(response);
    }
    
    //[Authorize]
    [HttpPut("{id:guid}")]
    public async Task<ActionResult<Guid>> UpdatePlan(Guid id, [FromBody] PlanRequest request, CancellationToken ct)
    {
        var planId = await _plansService.UpdatePlanAsync(id, request.Exercises, request.Description, ct);
        
        return Ok(planId);
    }
    
    //[Authorize]
    [HttpDelete("{id:guid}")]
    public async Task<ActionResult<Guid>> DeletePlan(Guid id, CancellationToken ct)
    {
        var planId = await _plansService.DeletePlanAsync(id, ct);
        
        return Ok(planId);
    }
    
}