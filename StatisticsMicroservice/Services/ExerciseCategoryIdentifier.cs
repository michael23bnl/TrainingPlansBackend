using StatisticsMicroservice.Services.Interfaces;

namespace StatisticsMicroservice.Models;

using Microsoft.ML;
using Microsoft.ML.Data;
using System.Collections.Generic;
using System.IO;

public class ExeriseCategoryIdentifier : IExerciseCategoryIdentifier
{
    private const string _modelPath = "exerciseCategoryModel.zip";
    private const string trainingDataPath = "Data/TrainingData.json";
    private readonly MLContext _mlContext;
    private ITransformer _model;
    private readonly ITrainingDataLoader _loader;
    private PredictionEngine<TrainingData, Prediction> _predictionEngine;

    public ExeriseCategoryIdentifier(ITrainingDataLoader loader)
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

    public void TrainModel()
    {
        var trainingData = _loader.LoadFromJsonFile(trainingDataPath);
        var dataView = _mlContext.Data.LoadFromEnumerable(trainingData);

        var pipeline = _mlContext.Transforms.Conversion
            .MapValueToKey("Label", nameof(TrainingData.Category))
            .Append(_mlContext.Transforms.Text.FeaturizeText("Features", nameof(TrainingData.ExercisesDescription)))
            .Append(_mlContext.MulticlassClassification.Trainers.SdcaMaximumEntropy("Label", "Features"))
            .Append(_mlContext.Transforms.Conversion.MapKeyToValue("PredictedLabel"));

        _model = pipeline.Fit(dataView);

        _mlContext.Model.Save(_model, dataView.Schema, _modelPath);

        _predictionEngine = _mlContext.Model.CreatePredictionEngine<TrainingData, Prediction>(_model);
    }

    public string PredictCategory(string exercisesDescription)
    {
        if (_predictionEngine == null)
        {
            LoadModel();
        }

        var prediction = _predictionEngine.Predict(new TrainingData { ExercisesDescription = exercisesDescription });
        return prediction.PredictedCategory;
    }
}
