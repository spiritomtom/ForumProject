using ForumProject.MLApi.MLModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.ML;
using System;
using System.IO;

namespace ForumProject.MLApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AnalyzeController : ControllerBase
    {
        private static readonly object _lock = new object();
        private static PredictionEngine<InputModel, OutputModel> _engine;
        private static string MLNetModelPath = Path.GetFullPath("MLModel.mlnet");

        static AnalyzeController()
        {
            var mlContext = new MLContext();
            lock (_lock)
            {
                var model = mlContext.Model.Load(MLNetModelPath, out var _);
                _engine = mlContext.Model.CreatePredictionEngine<InputModel, OutputModel>(model);
            }
        }

        [HttpPost]
        public ActionResult<OutputModel> Post([FromBody] InputModel input)
        {
            if (string.IsNullOrWhiteSpace(input?.Text))
                return BadRequest();

            var prediction = _engine.Predict(input);
            return Ok(prediction);
        }
    }
}