using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using api.Models;

namespace api.Services
{
    public class GPTService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;

        public GPTService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _apiKey = Environment.GetEnvironmentVariable("OPENAI_API_KEY") ?? "";
            if (string.IsNullOrEmpty(_apiKey))
                throw new Exception("OpenAI API key is not set.");
        }

        public async Task<AnalysisResult> GetAnalysisAsync(string agent, string customer)
        {
            // Settings prompt to GPT.
            var url = "https://api.openai.com/v1/chat/completions";
            var prompt = $"Analyze the conversation segment in detail with focus on strengths and understanding. " +
                         $"Return a JSON object with keys: errorsAgent, CustomerFeelAndUnderstanding, tipsAgentImprove. " +
                         $"Agent: \"{agent}\" Customer: \"{customer}\".";

            // Settings model to use and roles for best api response performance.
            var requestBody = new
            {
                model = "gpt-3.5-turbo",
                messages = new object[]
                {
                    new { role = "system", content = "You are an assistant for analyzing conversation interactions." },
                    new { role = "user", content = prompt }
                },
                temperature = 0.5
            };

            // handling http request
            var requestJson = JsonSerializer.Serialize(requestBody);
            var requestMessage = new HttpRequestMessage(HttpMethod.Post, url);
            requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _apiKey);
            requestMessage.Content = new StringContent(requestJson, Encoding.UTF8, "application/json");

            var response = await _httpClient.SendAsync(requestMessage);
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();

            // processing response from GPT API.
            using var document = JsonDocument.Parse(responseString);
            var choices = document.RootElement.GetProperty("choices");
            if (choices.GetArrayLength() > 0)
            {
                var content = choices[0].GetProperty("message").GetProperty("content").GetString();
                try
                {
                    var analysis = JsonSerializer.Deserialize<AnalysisResult>(content ?? "", new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                    if (analysis != null) return analysis;
                }
                catch
                {
                    return new AnalysisResult
                    {
                        ErrorsAgent = "Error parsing GPT response.",
                        CustomerFeelAndUnderstanding = "Error parsing GPT response.",
                        TipsAgentImprove = "Error parsing GPT response."
                    };
                }
            }
            // in case not analysis
            return new AnalysisResult
            {
                ErrorsAgent = "No analysis returned.",
                CustomerFeelAndUnderstanding = "No analysis returned.",
                TipsAgentImprove = "No analysis returned."
            };
        }
    }
}
