using Microsoft.EntityFrameworkCore;
using TrainingPlans.Entities;
using TrainingPlans.Models;
using TrainingPlans.Repositories.Interfaces;

namespace TrainingPlans.Repositories;

public class ExercisesRepository : IExercisesRepository
{
    private readonly PlansDbContext _context;

    public ExercisesRepository(PlansDbContext context)
    {
        _context = context;
    }

    public async Task<Guid> Create(ExerciseModel exercise)
    {
        var exerciseEntity = new ExerciseEntity
        {
            Id = exercise.Id,
            Name = exercise.Name,
            MuscleGroup = exercise.MuscleGroup,
        };
        await _context.Exercises.AddAsync(exerciseEntity);
        await _context.SaveChangesAsync();
        return exerciseEntity.Id;
    }
    
    public async Task<List<ExerciseModel>> GetAll()
    {
        var exerciseEntities = await _context.Exercises
            .AsNoTracking()
            .ToListAsync();
        var exercises = exerciseEntities
            .Select(e => ExerciseModel.Create(e.Id, e.Name, e.MuscleGroup, e.IsPrepared, e.CreatedBy).exerciseModel)
            .ToList();
        return exercises;
    }
    
    public async Task<List<ExerciseModel>> GetAllPrepared()
    {
        var exerciseEntities = await _context.Exercises
            .Where(e => e.IsPrepared == true)
            .AsNoTracking()
            .ToListAsync();
        var exercises = exerciseEntities
            .Select(e => ExerciseModel.Create(e.Id, e.Name, e.MuscleGroup, e.IsPrepared, e.CreatedBy).exerciseModel)
            .ToList();
        return exercises;
    }
    
    public async Task<List<ExerciseModel>> GetAllSelfMade(Guid userId)
    {
        var exerciseEntities = await _context.Exercises
            .Where(e => e.CreatedBy == userId)
            .AsNoTracking()
            .ToListAsync();
        var exercises = exerciseEntities
            .Select(e => ExerciseModel.Create(e.Id, e.Name, e.MuscleGroup, e.IsPrepared, e.CreatedBy).exerciseModel)
            .ToList();
        return exercises;
    }

    public async Task<ExerciseModel> Get(Guid id)
    {
        var exerciseEntity = await _context.Exercises.FirstOrDefaultAsync(e => e.Id == id);
        var exercise = ExerciseModel
            .Create(exerciseEntity.Id, exerciseEntity.Name, exerciseEntity.MuscleGroup, exerciseEntity.IsPrepared, exerciseEntity.CreatedBy)
            .exerciseModel;
        return exercise;
    }

    public async Task<Guid> Update(Guid id, string name, string muscleGroup)
    {
        var exercise = await _context.Exercises.FirstOrDefaultAsync(e => e.Id == id);
        
        if (exercise != null)
        {
            exercise.Name = name;
            exercise.MuscleGroup = muscleGroup;
        }

        await _context.SaveChangesAsync();
        return exercise.Id;
    }

    public async Task<Guid> Delete(Guid id)
    {
        var exercise = await _context.Exercises.FirstOrDefaultAsync(e => e.Id == id);
        
        if (exercise != null)
        {
            _context.Exercises.Remove(exercise);
        }

        return id;
    }
    
}