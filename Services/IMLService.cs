using Lung.Analysis.System.Models;

namespace Lung.Analysis.System.Services;

public interface IMLService
{
    Task<PredictionResult> PredictAsync(IFormFile imageFile, string patientId = null);
}
