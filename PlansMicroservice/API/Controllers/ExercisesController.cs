using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TrainingPlans.API.DTO;
using TrainingPlans.Domain.Abstractions;
using TrainingPlans.Domain.Entities;

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
    
    [Authorize("Create")]
    [HttpPost("create")]
    public async Task<ActionResult<Guid>> CreateExerciseAsync([FromBody] ExerciseRequest request, CancellationToken ct)
    {
        var exerciseId = await _exercisesService.CreateExerciseAsync(request.Name, request.MuscleGroup, ct);

        return Ok(exerciseId);
    }
    
    [Authorize]
    [HttpGet("get/all")]
    public async Task<ActionResult<List<ExerciseEntity>>> GetAllExercisesAsync(CancellationToken ct)
    {
        var exercises = await _exercisesService.GetAllExercisesAsync(ct);
        
        return Ok(exercises);
    }
    
    [Authorize]
    [HttpGet("get/{id:guid}")]
    public async Task<ActionResult<Guid>> GetExerciseAsync(Guid id, CancellationToken ct)
    {
        var exercise = await _exercisesService.GetExerciseAsync(id, ct);

        if (exercise is null)
        {
            return NotFound("Упражнения с указанным Id не найдено");
        }
        
        return Ok(exercise);
    }
    
    [Authorize]
    [HttpGet("get/{name}")]
    public async Task<ActionResult<ExerciseEntity>> GetExerciseByNameAsync(string name, CancellationToken ct)
    {
        var exercise = await _exercisesService.GetExerciseByNameAsync(name, ct);
        
        if (exercise is null)
        {
            return NotFound("Упражнения с указанным названием не найдено");
        }
        
        return exercise;
    }
    
    [Authorize]
    [HttpGet("get/muscle-group/{muscleGroup}")]
    public async Task<List<ExerciseEntity>> GetExercisesByMuscleGroupAsync(string muscleGroup, CancellationToken ct)
    {
        var exercises = await _exercisesService.GetExercisesByMuscleGroupAsync(muscleGroup, ct);
        
        return exercises;
    }
    
    [Authorize]
    [HttpGet("get/all-by-muscle-group")]
    public async Task<ActionResult<Dictionary<string, List<ExerciseEntity>>>> GetAllExercisesByMuscleGroupAsync(CancellationToken ct)
    {
        var exercises = await _exercisesService.GetAllExercisesByMuscleGroupAsync(ct);
        
        return exercises;
    }
    
    [Authorize("Update")]
    [HttpPut("update")]
    public async Task<ActionResult<Guid>> UpdateExercise(Guid id, [FromBody] ExerciseRequest request, CancellationToken ct)
    {
        var exerciseId = await _exercisesService
            .UpdateExerciseAsync(id, request.Name, request.MuscleGroup, ct);
        
        return Ok(exerciseId);
    }
    
    [Authorize("Delete")]
    [HttpDelete("delete")]
    public async Task<ActionResult<Guid>> DeleteExercise(Guid id, CancellationToken ct)
    {
        var exerciseId = await _exercisesService.DeleteExerciseAsync(id, ct);
        
        return Ok(exerciseId);
    }
}