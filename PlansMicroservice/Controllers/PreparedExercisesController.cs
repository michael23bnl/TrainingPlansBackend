using Microsoft.AspNetCore.Mvc;

using TrainingPlans.Contracts;
using TrainingPlans.Models;
using TrainingPlans.Repositories.Interfaces;
using UserMicroservice.Repositories.Interfaces;

namespace TrainingPlans.Controllers;

[ApiController]
[Route("/api/[controller]")]

public class PreparedExercisesController : ControllerBase
{
    private readonly IExercisesRepository _exercisesRepository;

    public PreparedExercisesController(IExercisesRepository exercisesRepository)
    {
        _exercisesRepository = exercisesRepository;
    }
    
    [HttpPost("create")]
    public async Task<ActionResult<Guid>> CreatePreparedExercise([FromBody] ExerciseRequest request)
    {

        var (exercise, response) = ExerciseModel
            .Create(Guid.NewGuid(), request.name, request.muscleGroup, true, null);

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
        return Ok(await _exercisesRepository.GetAllPrepared());
    }
    [HttpGet("get/{id:guid}")]
    public async Task<ActionResult<Guid>> GetExercise(Guid exerciseId)
    {
        return Ok(await _exercisesRepository.Get(exerciseId));
    }
    
    [HttpGet("get/{name}")]
    public async Task<ExerciseModel> GetExerciseByName(string name)
    {
        var exercise = await _exercisesRepository.GetByName(name);
        
        return exercise;
    }

    [HttpGet("get/category/{category}")]
    public async Task<List<ExerciseModel>> GetExercisesByCategory(string muscleGroup)
    {
        var exercises = await _exercisesRepository.GetByCategory(muscleGroup);
        
        return exercises;
    }
    
    [HttpGet("get/all/categorized")]
    public async Task<ActionResult<Dictionary<string, List<ExerciseModel>>>> GetAllExercisesCategorized()
    {
        var exercises = await _exercisesRepository.GetAllCategorized();
        
        return exercises;
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