using System.Text;
using System.Text.Json;
using ForumProject.MLModels;

namespace ForumProject.Services
{
    public class SentimentService
    {
        private readonly HttpClient _httpClient;

        public SentimentService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
        }

        public async Task<bool> IsToxicAsync(string comment)
        {
            var input = new InputModel { Text = comment };
            var content = new StringContent(JsonSerializer.Serialize(input), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("http://localhost:5000/analyze", content);
            if (!response.IsSuccessStatusCode) return false;

            var resultJson = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<OutputModel>(resultJson);
            return result?.PredictedLabel ?? false;
        }
    }
}