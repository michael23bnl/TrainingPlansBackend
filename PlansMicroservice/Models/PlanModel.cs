using System.Text.Json.Serialization;

namespace TrainingPlans.Models;

public class PlanModel
{
    public Guid Id { get; set; }
    
    public string? Name { get; set; }
    
    public List<ExerciseModel> Exercises {get; set;}
    
    public bool IsPrepared { get; set; } 
    
    public Guid? CreatedBy { get; set; }
    

    private PlanModel(Guid id, string? name, List<ExerciseModel> exercises, bool isPrepared, Guid? createdBy)
    {
        Id = id;
        Name = name;
        Exercises = exercises;
        IsPrepared = isPrepared;
        CreatedBy = createdBy;
    }

    public static (PlanModel? planModel, string response) Create(Guid id, string? name, List<ExerciseModel>? exercises, bool isPrepared, Guid? createdBy)
    {
        var response = "Plan must have exercises";
        if (exercises.Count > 0)
        {
            var planModel = new PlanModel(id, name, exercises, isPrepared, createdBy);
            response = "Plan has been created";
            return (planModel, response);
        }

        return (null, response);
    }
}