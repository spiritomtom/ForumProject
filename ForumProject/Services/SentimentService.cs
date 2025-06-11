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
        var input = new { Text = comment };
        var response = await _httpClient.PostAsJsonAsync("analyze", input);
        if (!response.IsSuccessStatusCode)
            throw new Exception("MLApi failed");

        var json = await response.Content.ReadFromJsonAsync<AnalyzeResponse>();
        return json.PredictedLabel;
    }

    private class AnalyzeResponse
    {
        public bool PredictedLabel { get; set; }
        public float Probability { get; set; }
        public float Score { get; set; }
    }
}