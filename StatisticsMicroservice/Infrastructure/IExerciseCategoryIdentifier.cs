namespace StatisticsMicroservice.Services.Interfaces;

public interface IExerciseCategoryIdentifier
{
    public void TrainModel();
    public string PredictCategory(string exercisesText);

}