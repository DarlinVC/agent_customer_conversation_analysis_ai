using System.Text.Json.Serialization;

namespace api.Models
{
    public class Interaction
    {
        [JsonPropertyName("agent")]
        public string? Agent { get; set; }
        
        [JsonPropertyName("customer")]
        public string? Customer { get; set; }
        
        [JsonPropertyName("analysis")]
        public AnalysisResult? Analysis { get; set; }
    }
}
