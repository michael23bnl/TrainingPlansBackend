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
    
    //[Authorize("Create")]
    [HttpPost]
    public async Task<ActionResult<Guid>> CreateAsync([FromBody] ExerciseRequest request, CancellationToken ct)
    {
        var exerciseId = await _exercisesService.CreateExerciseAsync(request.Name, request.MuscleGroup,
            request.Description, ct);

        return Ok(exerciseId);
    }
    
    //[Authorize]
    [HttpGet("/category")] 
    public async Task<List<ExerciseEntity>> GetAsync([FromQuery] string? muscleGroup, CancellationToken ct)
    {
        List<ExerciseEntity> exercises;
        
        if (!string.IsNullOrEmpty(muscleGroup))
            exercises = await _exercisesService.GetExercisesByMuscleGroupAsync(muscleGroup, ct);
        else
            exercises = await _exercisesService.GetAllExercisesAsync(ct);

        return exercises;
    }
    
    //[Authorize]
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<Guid>> GetAsync(Guid id, CancellationToken ct)
    {
        var exercise = await _exercisesService.GetExerciseAsync(id, ct);

        if (exercise is null)
        {
            return NotFound("Упражнения с указанным Id не найдено");
        }
        
        return Ok(exercise);
    }
    
    //[Authorize]
    [HttpGet("{name}")]
    public async Task<ActionResult<ExerciseEntity>> GetByNameAsync(string name, CancellationToken ct)
    {
        var exercise = await _exercisesService.GetExerciseByNameAsync(name, ct);
        
        if (exercise is null)
        {
            return NotFound("Упражнения с указанным названием не найдено");
        }
        
        return exercise;
    }

    //[Authorize]
    [HttpGet("grouped")]
    public async Task<ActionResult<Dictionary<string, List<ExerciseEntity>>>> GetAllByMuscleGroupAsync(CancellationToken ct)
    {
        var exercises = await _exercisesService.GetAllExercisesByMuscleGroupAsync(ct);
        
        return exercises;
    }
    
    //[Authorize("Update")]
    [HttpPut]
    public async Task<ActionResult<Guid>> UpdateAsync(Guid id, [FromBody] ExerciseRequest request, CancellationToken ct)
    {
        var exerciseId = await _exercisesService
            .UpdateExerciseAsync(id, request.Name, request.MuscleGroup, request.Description, ct);
        
        return Ok(exerciseId);
    }
    
    //[Authorize("Delete")]
    [HttpDelete]
    public async Task<ActionResult<Guid>> DeleteAsync(Guid id, CancellationToken ct)
    {
        var exerciseId = await _exercisesService.DeleteExerciseAsync(id, ct);
        
        return Ok(exerciseId);
    }
}