using Microsoft.AspNetCore.Mvc;
using TrainingPlans.Repositories;

namespace TrainingPlans.Controllers;

[ApiController]
[Route("/api/[controller]")]

public class SelfMadePlansController : ControllerBase
{
    private readonly SelfMadePlansRepository _selfMadePlansRepository;

    public SelfMadePlansController(SelfMadePlansRepository selfMadePlansRepository)
    {
        _selfMadePlansRepository = selfMadePlansRepository;
    }
    
    
}