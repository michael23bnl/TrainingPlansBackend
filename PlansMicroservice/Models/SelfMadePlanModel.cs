namespace TrainingPlans.Models;

public class SelfMadePlanModel
{
    public Guid Id { get; set; }
    
    public Guid UserId { get; set; }
    
    public List<ExerciseModel> Exercises {get; set;}
    

    private SelfMadePlanModel(Guid id, Guid userId, List<ExerciseModel> exercises)
    {
        Id = id;
        Exercises = exercises;
    }

    public static (SelfMadePlanModel? planModel, string response) Create(Guid id, Guid userId, List<ExerciseModel> exercises)
    {
        var response = "Plan must have exercises";
        if (exercises.Count > 0)
        {
            var selfMadePlanModel = new SelfMadePlanModel(id, userId, exercises);
            response = "Plan has been created";
            return (selfMadePlanModel, response);
        }

        return (null, response);
    }
}