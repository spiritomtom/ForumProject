using Microsoft.AspNetCore.Mvc;
using ForumProject.MLModels;
using Microsoft.ML;

namespace ForumProject.MLApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AnalyzeController : ControllerBase
    {
        private static readonly object _lock = new object();
        private static PredictionEngine<InputModel, OutputModel> _engine;

        static AnalyzeController()
        {
            var mlContext = new MLContext();
            lock (_lock)
            {
                var model = mlContext.Model.Load("MLModel.zip", out var _);
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