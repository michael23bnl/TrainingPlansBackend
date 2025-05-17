using Microsoft.ML.Data;

namespace StatisticsMicroservice.Models;

public class Prediction
{
    [ColumnName("PredictedLabel")]
    public string PredictedCategory { get; set; }
    
    public float[] Score { get; set; }
}