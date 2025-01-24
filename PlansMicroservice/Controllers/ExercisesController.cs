using Microsoft.AspNetCore.Mvc;

using TrainingPlans.Contracts;
using TrainingPlans.Models;
using TrainingPlans.Repositories.Interfaces;
using UserMicroservice.Repositories.Interfaces;

namespace TrainingPlans.Controllers;

[ApiController]
[Route("/api/[controller]")]

public class ExercisesController : ControllerBase
{
    private readonly IExercisesRepository _exercisesRepository;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IJwtExtractor _jwtExtractor;

    public ExercisesController(IExercisesRepository exercisesRepository, IHttpContextAccessor httpContextAccessor, IJwtExtractor jwtExtractor)
    {
        _exercisesRepository = exercisesRepository;
        _httpContextAccessor = httpContextAccessor;
        _jwtExtractor = jwtExtractor;
    }
    [HttpPost("create")]
    public async Task<ActionResult<Guid>> CreateExercise([FromBody] ExerciseRequest request)
    {
        
        var token = _httpContextAccessor.HttpContext?.Request.Cookies["suchatastycookie"];

        var userId = Guid.Parse(_jwtExtractor.ExtractUserIdFromJwtToken(token));

        var (exercise, response) = ExerciseModel
            .Create(Guid.NewGuid(), request.name, request.muscleGroup, false, userId);

        if (response != "Exercise has been created")
        {
            return BadRequest(response);
            
        } 
        var exerciseId = await _exercisesRepository.Create(exercise);
        return Ok(exerciseId);
    }
    
    [HttpGet("get/all")]
    public async Task<ActionResult<List<ExerciseModel>>> GetAllExercises(Guid userId)
    {
        return Ok(await _exercisesRepository.GetAllSelfMade(userId));
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