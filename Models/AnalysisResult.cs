using System.Text.Json.Serialization;

namespace MyApi.Models
{
    public class AnalysisResult
    {
        [JsonPropertyName("errorsAgent")]
        public string? ErrorsAgent { get; set; }
        
        [JsonPropertyName("CustomerFeelAndUnderstanding")]
        public string? CustomerFeelAndUnderstanding { get; set; }
        
        [JsonPropertyName("tipsAgentImprove")]
        public string? TipsAgentImprove { get; set; }
    }
}
