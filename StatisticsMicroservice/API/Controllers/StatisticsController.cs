using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StatisticsMicroservice.Models;
using StatisticsMicroservice.Repositories;
using StatisticsMicroservice.Services.Interfaces;

namespace StatisticsMicroservice;

[ApiController]
[Route("/api/[controller]")]
public class StatisticsController : ControllerBase
{

    private readonly IStatisticsService _statisticsService;
    private readonly IHttpContextAccessor _httpContextAccessor;
    
    public StatisticsController(IStatisticsService statisticsService, 
    IHttpContextAccessor httpContextAccessor)
    {
        _statisticsService = statisticsService;
        _httpContextAccessor = httpContextAccessor;
    }
    
    private string? GetUserId()
    {
        var userId = _httpContextAccessor.HttpContext!.Request.Headers["X-User-Id"];
        return userId;
    }
    
    [Authorize]
    [HttpGet("get")]
    public async Task<List<Statistic>> GetUserStatistics()
    {
        var userId = Guid.Parse(GetUserId()!);
        var statistics = await _statisticsService.GetStatistics(userId);
        return statistics;
    }
    
}