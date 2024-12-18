using Microsoft.AspNetCore.Mvc;
using TrainingPlans.Contracts;
using TrainingPlans.Models;
using TrainingPlans.Repositories.Interfaces;

namespace TrainingPlans.Controllers;

[ApiController]
[Route("/api/[controller]")]

public class ExercisesController : ControllerBase
{
    private readonly IExercisesRepository _exercisesRepository;

    public ExercisesController(IExercisesRepository exercisesRepository)
    {
        _exercisesRepository = exercisesRepository;
    }
    [HttpPost("create")]
    public async Task<ActionResult<Guid>> CreateExercise([FromBody] ExerciseRequest request)
    {

        var (exercise, response) = ExerciseModel
            .Create(Guid.NewGuid(), request.name, request.muscleGroup);

        if (response != "Exercise has been created")
        {
            return BadRequest(response);
            
        } 
        var exerciseId = await _exercisesRepository.Create(exercise);
        return Ok(exerciseId);
    }
    
    [HttpGet("get/all")]
    public async Task<ActionResult<List<ExerciseModel>>> GetAllExercises()
    {
        return Ok(await _exercisesRepository.GetAll());
    }
    [HttpGet("get")]
    public async Task<ActionResult<Guid>> GetExercise(Guid exerciseId)
    {
        return Ok(await _exercisesRepository.Get(exerciseId));
    }
    
    [HttpPut("update")]
    public async Task<ActionResult<Guid>> UpdateExercise(Guid exerciseId, [FromBody] ExerciseRequest request)
    {
        return Ok(await _exercisesRepository.Update(exerciseId, request.name, request.muscleGroup));
    }
    [HttpDelete("delete")]
    public async Task<ActionResult<Guid>> DeleteExercise(Guid exerciseId)
    {
        return Ok(await _exercisesRepository.Delete(exerciseId));
    }
}