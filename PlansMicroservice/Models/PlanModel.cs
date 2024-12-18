using System.Text.Json.Serialization;

namespace TrainingPlans.Models;

public class PlanModel
{
    public Guid Id { get; set; }
    
    public List<ExerciseModel> Exercises {get; set;}
    

    private PlanModel(Guid id, List<ExerciseModel> exercises)
    {
        Id = id;
        Exercises = exercises;
    }

    public static (PlanModel? planModel, string response) Create(Guid id, List<ExerciseModel> exercises)
    {
        var response = "Plan must have exercises";
        if (exercises.Count > 0)
        {
            var planModel = new PlanModel(id, exercises);
            response = "Plan has been created";
            return (planModel, response);
        }

        return (null, response);
    }
}