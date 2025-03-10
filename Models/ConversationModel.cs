using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace api.Models
{
    public class ConversationModel
    {
        [JsonPropertyName("conversation")]
        public List<Interaction>? Conversation { get; set; }
        
        [JsonPropertyName("overallSatisfaction")]
        public string? OverallSatisfaction { get; set; }
    }
}
