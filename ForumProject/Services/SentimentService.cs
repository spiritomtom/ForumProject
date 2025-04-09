using Microsoft.ML;
using ForumProject.Models;

namespace ForumProject.Services
{
    public class SentimentService
    {
        private readonly PredictionEngine<CommentData, CommentPrediction> _predictionEngine;

        public SentimentService()
        {
            var mlContext = new MLContext();
            ITransformer mlModel = mlContext.Model.Load("nasbert_model.zip", out var _);
            _predictionEngine = mlContext.Model.CreatePredictionEngine<CommentData, CommentPrediction>(mlModel);
        }

        public bool Predict(string content)
        {
            var prediction = _predictionEngine.Predict(new CommentData { Content = content });
            return prediction.IsToxic;
        }
    }
}
