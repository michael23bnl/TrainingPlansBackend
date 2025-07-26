using Microsoft.EntityFrameworkCore;
using TrainingPlans.API.DTO;
using TrainingPlans.Domain.Models;
using TrainingPlans.Entities;
using TrainingPlans.Persistence.Repositories.Interfaces;

namespace TrainingPlans.Persistence.Repositories;

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
            .Select(e => ExerciseModel.Create(e.Id, e.Name, e.MuscleGroup).exerciseModel)
            .ToList();
        return exercises;
    }
    
    public async Task<List<ExerciseModel>> GetAllPrepared()
    {
        var exerciseEntities = await _context.Exercises
            .AsNoTracking()
            .ToListAsync();
        var exercises = exerciseEntities
            .Select(e => ExerciseModel.Create(e.Id, e.Name, 
                e.MuscleGroup).exerciseModel)
            .ToList();
        return exercises;
    }

    public async Task<ExerciseModel> Get(Guid id)
    {
        var exerciseEntity = await _context.Exercises.FirstOrDefaultAsync(e => e.Id == id);
        var exercise = ExerciseModel
            .Create(exerciseEntity.Id, exerciseEntity.Name, 
                exerciseEntity.MuscleGroup)
            .exerciseModel;
        return exercise;
    }

    public async Task<ExerciseModel> GetByName(string name)
    {
        var exerciseEntity = await _context.Exercises.FirstOrDefaultAsync(e => e.Name == name);
        
        var exercise = ExerciseModel.Create(exerciseEntity.Id, exerciseEntity.Name,
            exerciseEntity.MuscleGroup).exerciseModel;
        
        return exercise;
    }

    public async Task<List<ExerciseModel>> GetByCategory(string muscleGroup)
    {
        var exerciseEntities = await _context.Exercises
            .Where(e => e.MuscleGroup == muscleGroup).ToListAsync();

        var exercises = exerciseEntities.Select(et =>
            ExerciseModel.Create(et.Id, et.Name, et.MuscleGroup).exerciseModel).ToList();
        
        return exercises;
    }

    public async Task<Dictionary<string, List<CategorizedExercise>>> GetAllCategorized()
    {
        var exerciseEntities = await _context.Exercises
            .AsNoTracking()
            .ToListAsync();

        var categorizedExercises = new Dictionary<string, List<CategorizedExercise>>();

        foreach (var entity in exerciseEntities)
        {
            var categories = entity.MuscleGroup!
                .Split(',', StringSplitOptions.RemoveEmptyEntries)
                .Select(c => c.Trim())
                .ToList();

            var exerciseDto = new CategorizedExercise(entity.Name, categories);
            
            foreach (var category in categories)
            {
                if (!categorizedExercises.ContainsKey(category))
                {
                    categorizedExercises[category] = new List<CategorizedExercise>();
                }

                categorizedExercises[category].Add(exerciseDto);
            }
        }

        return categorizedExercises;
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