using StatisticsMicroservice.Models;
using StatisticsMicroservice.Services.Interfaces;
namespace StatisticsMicroservice;

using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class ExerciseCategoryController : ControllerBase
{
    private readonly IExerciseCategoryIdentifier _identifier;

    public ExerciseCategoryController(IExerciseCategoryIdentifier identifier)
    {
        _identifier = identifier;
    }

    [HttpPost("predict")]
    public ActionResult<string> Predict([FromBody] string exerciseDescription)
    {
        var category = _identifier.PredictCategory(exerciseDescription);
        return Ok(category);
    }

    [HttpPost("train")]
    public IActionResult Train()
    {
        _identifier.TrainModel();
        return Ok("Model trained successfully.");
    }
}
