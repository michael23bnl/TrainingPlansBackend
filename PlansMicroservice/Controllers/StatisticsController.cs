using System.Collections.Concurrent;
using Microsoft.AspNetCore.Mvc;
using TrainingPlans.Repositories.Interfaces;
using TrainingPlans.Services.Statistics;

namespace TrainingPlans.Controllers;

[ApiController]
[Route("/api/[controller]")]

public class StatisticsController : ControllerBase
{
    private readonly ICompletedPlansRepository _completedPlansRepository;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IStatisticsService _statisticsService;

    public StatisticsController(ICompletedPlansRepository completedPlansRepository,
        IHttpContextAccessor httpContextAccessor,
        IStatisticsService statisticsService)
    {
        _completedPlansRepository = completedPlansRepository;
        _httpContextAccessor = httpContextAccessor;
        _statisticsService = statisticsService;
    }
    
    private string? GetUserId()
    {
        var userId = _httpContextAccessor.HttpContext!.Request.Headers["X-User-Id"];
        return userId;
    }
    
    [HttpGet("get")]

    public async Task<ConcurrentDictionary<string, int>> GetStatistics()
    {
        var userId = Guid.Parse(GetUserId()!);
        var plans = await _completedPlansRepository.GetCompletedPlans(userId);
        
        var statistics = _statisticsService.GetStatistics(plans);

        return statistics;

    }
}