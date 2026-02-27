using Microsoft.AspNetCore.Mvc;
using TrainingPlans.Infrastructure.Elasticsearch;

namespace TrainingPlans.API.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class ElasticAdminController : ControllerBase
{
    private readonly IElasticAdmin _elasticAdmin;

    public ElasticAdminController(IElasticAdmin elasticAdmin)
    {
        _elasticAdmin = elasticAdmin;
    }
    
    [HttpGet]
    public async Task<ActionResult> GetAll(CancellationToken ct)
    {
        var docs = await _elasticAdmin.GetAllAsync(ct);

        return Ok(docs);
    }
}