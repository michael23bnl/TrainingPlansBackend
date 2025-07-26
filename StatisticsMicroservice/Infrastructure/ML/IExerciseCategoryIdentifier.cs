using Microsoft.ML.Data;

namespace StatisticsMicroservice.Infrastructure.ML;

public interface IExerciseCategoryIdentifier
{
    public void TrainModel();
    public string DefineCategory(string exercisesText);

    public MulticlassClassificationMetrics EvaluateModel();

    public void CrossValidateModel();

}