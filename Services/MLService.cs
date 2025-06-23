using Lung.Analysis.System.ML;
using Lung.Analysis.System.Models;
using Microsoft.Extensions.Options;
using Microsoft.ML.Data;
using Microsoft.Extensions.ML;
using Microsoft.ML;

namespace Lung.Analysis.System.Services;

public class MLService : IMLService
{
    private readonly PredictionEnginePool<ImageInputData, ImagePrediction> _predictionEnginePool;
    private readonly ModelConfig _config;

    public MLService(
        PredictionEnginePool<ImageInputData, ImagePrediction> predictionEnginePool,
        IOptions<ModelConfig> config)
    {
        _predictionEnginePool = predictionEnginePool;
        _config = config.Value;
    }

    public async Task<PredictionResult> PredictAsync(IFormFile imageFile, string patientId = null)
    {
        // Save the uploaded image to a temporary file
        var tempImagePath = Path.GetTempFileName();
        using (var stream = new FileStream(tempImagePath, FileMode.Create))
        {
            await imageFile.CopyToAsync(stream);
        }

        try
        {
            // Create input data
            var input = new ImageInputData { ImagePath = tempImagePath };

            // Make prediction
            var prediction = _predictionEnginePool.Predict(input);

            // Map to our result model
            var result = new PredictionResult
            {
                PatientId = patientId ?? Guid.NewGuid().ToString(),
                Timestamp = DateTime.UtcNow,
                Scores = new Dictionary<string, float>()
            };

            // Get the highest score
            var maxScore = prediction.Score.Max();
            var maxIndex = prediction.Score.ToList().IndexOf(maxScore);

            result.Prediction = _config.Labels[maxIndex];
            result.Probability = maxScore;

            // Add all scores
            for (int i = 0; i < _config.Labels.Length; i++)
            {
                result.Scores.Add(_config.Labels[i], prediction.Score[i]);
            }

            return result;
        }
        finally
        {
            // Clean up
            if (File.Exists(tempImagePath))
            {
                File.Delete(tempImagePath);
            }
        }
    }
}

// ML.NET input model
public class ImageInputData
{
    [LoadColumn(0)]
    public string ImagePath;

    [LoadColumn(1)]
    public string Label;
}

// ML.NET output model
public class ImagePrediction
{
    [ColumnName("softmax2")]
    public float[] Score;

    [ColumnName("grid")]
    public float[] PredictedLabel;
}