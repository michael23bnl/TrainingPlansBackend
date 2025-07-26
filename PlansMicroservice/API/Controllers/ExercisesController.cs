using Microsoft.AspNetCore.Mvc;
using TrainingPlans.API.DTO;
using TrainingPlans.Application.Services.Interfaces;
using TrainingPlans.Domain.Models;

namespace TrainingPlans.API.Controllers;

[ApiController]
[Route("/api/[controller]")]

public class ExercisesController : ControllerBase
{
    private readonly IExercisesService _exercisesService;

    public ExercisesController(IExercisesService exercisesService)
    {
        _exercisesService = exercisesService;
    }
    
    [HttpPost("create")]
    public async Task<ActionResult<Guid>> CreateExercise([FromBody] ExerciseRequest request)
    {
        var exerciseId = await _exercisesService.CreateExercise(request);
        if (exerciseId == Guid.Empty)
        {
            return BadRequest("Не удалось создать упражнение");
        }
        return Ok(exerciseId);
    }
    
    [HttpGet("get/all")]
    public async Task<ActionResult<List<ExerciseModel>>> GetAllExercises()
    {
        var exercises = await _exercisesService.GetAllExercises();
        return Ok(exercises);
    }
    [HttpGet("get/{id:guid}")]
    public async Task<ActionResult<Guid>> GetExercise(Guid exerciseId)
    {
        var exercise = await _exercisesService.GetExercise(exerciseId);
        return Ok(exercise);
    }
    
    [HttpGet("get/{name}")]
    public async Task<ExerciseModel> GetExerciseByName(string name)
    {
        var exercise = await _exercisesService.GetExerciseByName(name);
        
        return exercise;
    }

    [HttpGet("get/category/{category}")]
    public async Task<List<ExerciseModel>> GetExercisesByCategory(string muscleGroup)
    {
        var exercises = await _exercisesService.GetExercisesByCategory(muscleGroup);
        
        return exercises;
    }
    
    [HttpGet("get/all/categorized")]
    public async Task<ActionResult<Dictionary<string, List<CategorizedExercise>>>> GetAllExercisesCategorized()
    {
        var exercises = await _exercisesService.GetAllExercisesCategorized();
        
        return exercises;
    }
    
    [HttpPut("update")]
    public async Task<ActionResult<Guid>> UpdateExercise(Guid exerciseId, [FromBody] ExerciseRequest request)
    {
        var updatedExerciseId = await _exercisesService.UpdateExercise(exerciseId, request);
        return Ok(updatedExerciseId);
    }
    [HttpDelete("delete")]
    public async Task<ActionResult<Guid>> DeleteExercise(Guid exerciseId)
    {
        var deletedExerciseId = await _exercisesService.DeleteExercise(exerciseId);
        return Ok(deletedExerciseId);
    }
}