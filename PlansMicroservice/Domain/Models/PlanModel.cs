using System.Text.Json.Serialization;


namespace TrainingPlans.Domain.Models;

public class PlanModel
{
    public Guid Id { get; }
    
    public string? Category { get; }
    
    public List<ExerciseModel> Exercises {get; }
    
    public Guid? CreatedBy { get; }
    
    [JsonConstructor]
    private PlanModel(Guid id, string? category, List<ExerciseModel> exercises, Guid? createdBy)
    {
        Id = id;
        Category = category;
        Exercises = exercises;
        CreatedBy = createdBy;
    }

    public static (PlanModel? planModel, string response) Create(Guid id, string? name, 
        List<ExerciseModel>? exercises, Guid? createdBy)
    {
        var response = "Plan must have exercises";
        if (exercises.Count > 0)
        {
            var planModel = new PlanModel(id, name, exercises, createdBy);
            response = "Plan has been created";
            return (planModel, response);
        }

        return (null, response);
    }
}