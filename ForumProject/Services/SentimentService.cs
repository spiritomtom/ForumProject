using ForumProject.MLModels;

namespace ForumProject.Services;

public class SentimentService
{
    private readonly HttpClient _httpClient;

    public SentimentService(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient();
        _httpClient.BaseAddress = new Uri("http://localhost:5048/"); // MLApi URL
    }

    public async Task<bool> IsToxicAsync(string comment)
    {
        var input = new { Text = comment, Label = "0"};
        var response = await _httpClient.PostAsJsonAsync("analyze", input);
        if (!response.IsSuccessStatusCode)
            throw new Exception("MLApi failed");

        var json = await response.Content.ReadFromJsonAsync<OutputModel>();

        if (json == null)
            throw new Exception("Prediction result is null or invalid");

        float.TryParse(json.PredictedLabel, out var predictionResult);
       
        if (predictionResult > 0)
            return false;
        return true;
    }
}