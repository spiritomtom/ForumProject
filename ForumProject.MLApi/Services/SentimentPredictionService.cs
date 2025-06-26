using ForumProject.MLApi.MLModels;
using Microsoft.ML;

namespace ForumProject.MLApi.Services
{
    public class SentimentPredictionService
    {
        private readonly PredictionEngine<InputModel, OutputModel> _predictionEngine;

        public SentimentPredictionService()
        {
            var mlContext = new MLContext();
            var model = mlContext.Model.Load("MLModel.zip", out var modelInputSchema);
            _predictionEngine = mlContext.Model.CreatePredictionEngine<InputModel, OutputModel>(model);
        }

        public OutputModel Predict(string text)
        {
            var input = new InputModel { Text = text };
            return _predictionEngine.Predict(input);
        }
    }
}