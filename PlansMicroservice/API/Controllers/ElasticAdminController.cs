using Microsoft.AspNetCore.Mvc;
using Shared.DTO;
using TrainingPlans.API.DTO;
using TrainingPlans.Domain.Abstractions;

namespace TrainingPlans.API.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class ElasticAdminController : ControllerBase
{
    private readonly IElasticAdminService _elasticAdminService;

    public ElasticAdminController(IElasticAdminService elasticAdminService)
    {
        _elasticAdminService = elasticAdminService;
    }
    
    [HttpGet("documents")]
    public async Task<ActionResult<List<PlanResponse>>> GetAllAsync(CancellationToken ct)
    {
        var docs = await _elasticAdminService.GetAllAsync(ct);
        
        return Ok(docs);
    }

    [HttpGet("documents/{id:guid}")]
    public async Task<ActionResult<PlanResponse>> GetAsync(Guid id, CancellationToken ct)
    {
        var doc = await _elasticAdminService.GetAsync(id, ct);
        
        if (doc == null) return NotFound();
        
        return Ok(doc);
    }

    [HttpPost("index")]
    public async Task<IActionResult> CreateIndexAsync(CancellationToken ct)
    {
        await _elasticAdminService.CreateIndexAsync(ct);
        
        return Ok();
    }

    [HttpGet("documents/contains")]
    public async Task<ActionResult<bool>> ContainsDocumentsAsync(CancellationToken ct)
    {
        var contains = await _elasticAdminService.ContainsDocumentsAsync(ct);
        
        return Ok(contains);
    }

    [HttpPost("documents/plan")]
    public async Task<ActionResult> IndexPlanAsync([FromBody] PlanIndexRequest request, CancellationToken ct)
    {
        var success = await _elasticAdminService.IndexPlanAsync(request, ct);
        
        if (!success) return BadRequest();
        
        return Ok();
    }

    [HttpPost("documents/customplan")]
    public async Task<ActionResult> IndexCustomPlanAsync([FromBody] CustomPlanIndexRequest request, CancellationToken ct)
    {
        var success = await _elasticAdminService.IndexCustomPlanAsync(request, ct);
        
        if (!success) return BadRequest();
        
        return Ok();
    }

    [HttpPost("documents/plans")]
    public async Task<ActionResult> IndexPlansAsync([FromBody] List<PlanIndexRequest> request, CancellationToken ct)
    {
        var success = await _elasticAdminService.IndexPlansAsync(request, ct);
        
        if (!success) return BadRequest();
        
        return Ok();
    }

    [HttpPost("documents/customplans")]
    public async Task<ActionResult> IndexCustomPlansAsync([FromBody] List<CustomPlanIndexRequest> request, CancellationToken ct)
    {
        var success = await _elasticAdminService.IndexCustomPlansAsync(request, ct);
        
        if (!success) return BadRequest();
        
        return Ok();
    }

    [HttpDelete("documents/{id:guid}")]
    public async Task<ActionResult> RemoveAsync(Guid id, CancellationToken ct)
    {
        var success = await _elasticAdminService.RemoveAsync(id, ct);
        
        if (!success) return NotFound();
        
        return Ok();
    }

    [HttpDelete("documents")]
    public async Task<ActionResult<long?>> RemoveAllAsync(CancellationToken ct)
    {
        var deletedCount = await _elasticAdminService.RemoveAllAsync(ct);
        
        return Ok(deletedCount);
    }
}