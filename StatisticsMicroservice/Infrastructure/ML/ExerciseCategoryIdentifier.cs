using StatisticsMicroservice.Infrastructure.ML;
using StatisticsMicroservice.Models;
using StatisticsMicroservice.Services.Interfaces;

namespace StatisticsMicroservice.Infrastructure.ML;

using Microsoft.ML;
using Microsoft.ML.Data;
using System.Collections.Generic;
using System.IO;

public class ExerciseCategoryIdentifier : IExerciseCategoryIdentifier
{
    private const string _modelPath = "Infrastructure/ML/exerciseCategoryModel.zip";
    private const string trainingDataPath = "Infrastructure/ML/Data/TrainingData.json";
    private const string evaluatingDataPath = "Infrastructure/ML/Data/EvaluatingData.json";
    private readonly MLContext _mlContext;
    private DataOperationsCatalog.TrainTestData _trainTestSplit;
    private ITransformer _model;
    private readonly ITrainingDataLoader _loader;
    private PredictionEngine<TrainingData, Prediction> _predictionEngine;

    public ExerciseCategoryIdentifier(ITrainingDataLoader loader)
    {
        _mlContext = new MLContext();
        _loader = loader;
        if (File.Exists(_modelPath))
        {
            LoadModel();
        }
    }
    
    public void LoadModel()
    {
        using var stream = new FileStream(_modelPath, FileMode.Open, FileAccess.Read, FileShare.Read);
        _model = _mlContext.Model.Load(stream, out _);
        _predictionEngine = _mlContext.Model.CreatePredictionEngine<TrainingData, Prediction>(_model);
    }
    public string DefineCategory(string exercisesDescription)
    {
        if (_predictionEngine == null)
        {
            LoadModel();
        }

        var prediction = _predictionEngine.Predict(new TrainingData { ExercisesDescription = exercisesDescription });
        return prediction.PredictedCategory;
    }
    
    public void TrainModel()
    {
        var trainingData = _loader.LoadFromJsonFile(trainingDataPath);
        var dataView = _mlContext.Data.LoadFromEnumerable(trainingData);
    
        _trainTestSplit = _mlContext.Data.TrainTestSplit(dataView, testFraction: 0.2);

        var pipeline = _mlContext.Transforms.Conversion
            .MapValueToKey("Label", nameof(TrainingData.Category))
            .Append(_mlContext.Transforms.Text.FeaturizeText("Features", nameof(TrainingData.ExercisesDescription)))
            .Append(_mlContext.MulticlassClassification.Trainers.SdcaMaximumEntropy("Label", "Features"))
            .Append(_mlContext.Transforms.Conversion.MapKeyToValue("PredictedLabel"));

        _model = pipeline.Fit(_trainTestSplit.TrainSet);

        _mlContext.Model.Save(_model, dataView.Schema, _modelPath);

        _predictionEngine = _mlContext.Model.CreatePredictionEngine<TrainingData, Prediction>(_model);
    }

    public MulticlassClassificationMetrics EvaluateModel()
    {
        if (_model == null)
        {
            throw new InvalidOperationException("Модель не обучена. Сначала вызовите TrainModel().");
        }

        var predictions = _model.Transform(_trainTestSplit.TestSet);

        return _mlContext.MulticlassClassification.Evaluate(
            predictions,
            labelColumnName: "Label",
            predictedLabelColumnName: "PredictedLabel");
    }

    
    public void CrossValidateModel()
    {
        var fullData = _loader.LoadFromJsonFile(trainingDataPath);
        var dataView = _mlContext.Data.LoadFromEnumerable(fullData);

        var pipeline = _mlContext.Transforms.Conversion
            .MapValueToKey("Label", nameof(TrainingData.Category))
            .Append(_mlContext.Transforms.Text.FeaturizeText("Features", nameof(TrainingData.ExercisesDescription)))
            .Append(_mlContext.MulticlassClassification.Trainers.SdcaMaximumEntropy("Label", "Features"))
            .Append(_mlContext.Transforms.Conversion.MapKeyToValue("PredictedLabel"));

        var cvResults = _mlContext.MulticlassClassification.CrossValidate(dataView, pipeline, numberOfFolds: 5, labelColumnName: "Label");

        foreach (var result in cvResults)
        {
            Console.WriteLine($"Fold: {result.Fold}, MicroAccuracy: {result.Metrics.MicroAccuracy}, MacroAccuracy: {result.Metrics.MacroAccuracy}");
        }

        var avgMicroAccuracy = cvResults.Average(r => r.Metrics.MicroAccuracy);
        var avgMacroAccuracy = cvResults.Average(r => r.Metrics.MacroAccuracy);
        Console.WriteLine($"Average MicroAccuracy: {avgMicroAccuracy}");
        Console.WriteLine($"Average MacroAccuracy: {avgMacroAccuracy}");
    }
}
