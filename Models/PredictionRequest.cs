namespace Lung.Analysis.System.Models;

public class PredictionRequest
{
    public IFormFile ImageFile { get; set; }
    public string? PatientId { get; set; }
}
