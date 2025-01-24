using System.Text.Json.Serialization;


namespace TrainingPlans.Models;

public class ExerciseModel
{
    public Guid Id { get; set; }
    
    public string Name { get; set; }
    
    public string MuscleGroup { get; set; }

    public bool? IsPrepared { get; set; }
    
    public Guid? CreatedBy { get; set; }
    
    [JsonConstructor]
    private ExerciseModel(Guid id, string name, string muscleGroup, bool? isPrepared, Guid? createdBy)
    {
        Id = id;
        Name = name;
        MuscleGroup = muscleGroup;
        IsPrepared = isPrepared;
        CreatedBy = createdBy;
    }

    public static (ExerciseModel? exerciseModel, string response) Create(Guid id, string name, string muscleGroup, bool? isPrepared, Guid? createdBy)
    {
        var response = "Exercise must have a name";
        if (!string.IsNullOrEmpty(name))
        {
            var exerciseModel = new ExerciseModel(id, name, muscleGroup, isPrepared, createdBy);
            response = "Exercise has been created";
            return (exerciseModel, response);
        }

        return (null, response);
    }

}