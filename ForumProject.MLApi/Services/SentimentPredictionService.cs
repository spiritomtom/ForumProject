using ForumProject.MLApi.MLModels;
using Microsoft.ML;

namespace ForumProject.MLApi.Services
{
    public class SentimentPredictionService
    {
        private readonly MLContext _mlContext;
        private readonly ITransformer _model;
        private readonly PredictionEngine<InputModel, OutputModel> _predictionEngine;

        public SentimentPredictionService()
        {
            _mlContext = new MLContext();
            // Load your pre-trained model file
            _model = _mlContext.Model.Load("MLModel.zip", out var modelInputSchema);
            _predictionEngine = _mlContext.Model.CreatePredictionEngine<InputModel, OutputModel>(_model);
        }

        public OutputModel Predict(string text)
        {
            var input = new InputModel { Text = text };
            return _predictionEngine.Predict(input);
        }
    }
}