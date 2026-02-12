using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Auth;
using Shared.Pagination;
using TrainingPlans.Application.Abstractions;
using TrainingPlans.Domain.Entities;

namespace TrainingPlans.API.Controllers;

[ApiController]
[Route("[controller]")]
public class PlansSearchController : ControllerBase
{
    private readonly IElasticService _elasticService;
    private readonly IUserContextService _userContextService;

    public PlansSearchController(IElasticService elasticService, IUserContextService userContextService)
    {
        _elasticService = elasticService;
        _userContextService = userContextService;
    }
    
    [HttpPost("create-index")]
    public async Task<IActionResult> CreateIndexIfNotExistsAsync(string indexName, CancellationToken ct)
    {
        await _elasticService.CreateIndexIfNotExistsAsync(indexName, ct);
        
        return Ok();
    }

    [HttpGet]
    public async Task<bool> ContainsDocumentsAsync(string indexName, CancellationToken ct)
    {
        var contains = await _elasticService.ContainsDocumentsAsync(indexName, ct);

        return contains;
    }

    [HttpPost("add-plan")]
    public async Task<IActionResult> AddPlanAsync([FromBody] PlanEntity plan, CancellationToken ct)
    {
        var result = await _elasticService.AddOrUpdateAsync(plan, ct);
        
        return result ? Ok("План успешно добавлен") :
            StatusCode(500, "Ошибка при добавлении плана");
    }
    
    [HttpPut("update-plan")]
    public async Task<IActionResult> UpdatePlanAsync([FromBody] PlanEntity plan, CancellationToken ct)
    {
        var result = await _elasticService.AddOrUpdateAsync(plan, ct);
        
        return result ? Ok("План успешно обновлён") :
            StatusCode(500, "Ошибка при обновлении плана");
    }

    [HttpDelete("delete-plan")]
    public async Task<IActionResult> DeletePlanAsync(string id, CancellationToken ct)
    {
        var result = await _elasticService.RemoveAsync(id, ct);
        
        return Ok(result);
    }

    [HttpPost("add-all-plans")]
    public async Task<IActionResult> AddAllPlansAsync(CancellationToken ct)
    {
        await _elasticService.AddOrUpdateBulkAsync(ct);
        
        return Ok();
    }

    [HttpDelete("delete-all-plans")]
    public async Task<IActionResult> DeleteAllPlansAsync(CancellationToken ct)
    {
        var deletedDocuments = await _elasticService.RemoveAllAsync(ct);
        
        return Ok(deletedDocuments);
    }

    [HttpGet("get-plan")]
    public async Task<IActionResult> GetPlanAsync(string id, CancellationToken ct)
    {
        var plan = await _elasticService.GetAsync(id, ct);
        
        return Ok(plan);
    }

    [HttpGet("get-all-plans")]
    public async Task<IActionResult> GetAllPlansAsync(CancellationToken ct)
    {
        var plans = await _elasticService.GetAllAsync(ct);
        
        return Ok(plans);
    }

    [HttpGet("search/{query}")]
    public async Task<IActionResult> SearchPlansAsync(string query, [FromQuery] PlanParameters planParameters, CancellationToken ct)
    {

        if (string.IsNullOrWhiteSpace(query))
        {
            return BadRequest("Введите данные для фильтрации");
        }
        
        var results = await _elasticService.SearchPlansAsync(query, planParameters, ct);

        return Ok(new
        {
            totalCount = results.totalCount,
            plans = results.plans
        });
    }

    //[Authorize]
    [HttpGet("search/custom-plans/{query}")]
    public async Task<IActionResult> SearchThroughCustomPlansAsync(string query,
        [FromQuery] PlanParameters planParameters, CancellationToken ct)
    {
        if (string.IsNullOrWhiteSpace(query))
        {
            return BadRequest("Введите данные для фильтрации");
        }

        var userId = _userContextService.GetUserId();
        var results = await _elasticService.SearchThroughCustomPlansAsync(query, userId, planParameters, ct);

        return Ok(new
        {
            totalCount = results.totalCount,
            plans = results.plans
        });
    }

    //[Authorize]
    [HttpGet("search/completed-plans/{query}")]
    public async Task<IActionResult> SearchThroughCompletedPlansAsync(string query,
        [FromQuery] PlanParameters planParameters, CancellationToken ct)
    {
        if (string.IsNullOrWhiteSpace(query))
        {
            return BadRequest("Введите данные для фильтрации");
        }

        var userId = _userContextService.GetUserId();
        var results = await _elasticService.SearchThroughCompletedPlansAsync(query, userId, planParameters, ct);

        return Ok(new
        {
            totalCount = results.totalCount,
            plans = results.plans
        });
    }
}