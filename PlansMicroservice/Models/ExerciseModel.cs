using System.Text.Json.Serialization;

namespace TrainingPlans.Models;

public class ExerciseModel
{
    public Guid Id { get; set; }
    
    public string Name { get; set; }
    
    public string MuscleGroup { get; set; }
    
    [JsonConstructor]
    private ExerciseModel(Guid id, string name, string muscleGroup)
    {
        Id = id;
        Name = name;
        MuscleGroup = muscleGroup;
    }

    public static (ExerciseModel? exerciseModel, string response) Create(Guid id, string name, string muscleGroup)
    {
        var response = "Exercise must have a name";
        if (!string.IsNullOrEmpty(name))
        {
            var exerciseModel = new ExerciseModel(id, name, muscleGroup);
            response = "Exercise has been created";
            return (exerciseModel, response);
        }

        return (null, response);
    }
}