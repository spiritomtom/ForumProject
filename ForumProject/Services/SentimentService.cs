using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

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
            var payload = new { text = comment };
            var content = new StringContent(JsonSerializer.Serialize(payload), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("http://localhost:5000/analyze", content); // or actual NAS-BERT endpoint
            if (!response.IsSuccessStatusCode) return false;

            var resultJson = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<SentimentResult>(resultJson);
            return result?.IsToxic ?? false;
        }
    }

    public class SentimentResult
    {
        public bool IsToxic { get; set; }
    }
}
