using Lung.Analysis.System.ML;
using Lung.Analysis.System.Models;
using Lung.Analysis.System.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

[ApiController]
[Route("api/[controller]")]
public class TBDetectionController : ControllerBase
{
    private readonly IMLService _mlService;
    private readonly ILogger<TBDetectionController> _logger;

    public TBDetectionController(
        IMLService mlService,
        ILogger<TBDetectionController> logger)
    {
        _mlService = mlService;
        _logger = logger;
    }

    [HttpPost("predict")]
    public async Task<IActionResult> Predict([FromForm] PredictionRequest request)
    {
        try
        {
            if (request.ImageFile == null || request.ImageFile.Length == 0)
            {
                return BadRequest("No image file uploaded.");
            }

            // Check file extension
            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png" };
            var extension = Path.GetExtension(request.ImageFile.FileName).ToLowerInvariant();
            if (!allowedExtensions.Contains(extension))
            {
                return BadRequest("Invalid file type. Only JPG, JPEG, and PNG are allowed.");
            }

            // Process the image
            var result = await _mlService.PredictAsync(request.ImageFile, request.PatientId);

            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error during prediction");
            return StatusCode(500, "An error occurred while processing your request.");
        }
    }

    [HttpGet("model-info")]
    public IActionResult GetModelInfo([FromServices] IOptions<ModelConfig> config)
    {
        return Ok(new
        {
            config.Value.Labels,
            config.Value.ImageWidth,
            config.Value.ImageHeight,
            LastModified = System.IO.File.GetLastWriteTimeUtc(config.Value.ModelPath)
        });
    }
}