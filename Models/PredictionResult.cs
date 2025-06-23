namespace Lung.Analysis.System.Models;

public class PredictionResult
{
    public string Prediction { get; set; }
    public float Probability { get; set; }
    public Dictionary<string, float> Scores { get; set; }
    public string PatientId { get; set; }
    public DateTime Timestamp { get; set; }
}
