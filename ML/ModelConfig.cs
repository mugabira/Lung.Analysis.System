namespace Lung.Analysis.System.ML;

public class ModelConfig
{
    public string ModelPath { get; set; }
    public string[] Labels { get; set; }
    public int ImageWidth { get; set; } = 224;
    public int ImageHeight { get; set; } = 224;
}
