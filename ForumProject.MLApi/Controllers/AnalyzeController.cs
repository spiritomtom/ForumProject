using ForumProject.MLApi.MLModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.ML;

namespace ForumProject.MLApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AnalyzeController : ControllerBase
    {
        private static readonly object Lock = new object();
        private static PredictionEngine<InputModel, OutputModel> _engine;
        private static readonly string MlNetModelPath = Path.GetFullPath("MLModel.mlnet");

        static AnalyzeController()
        {
            var mlContext = new MLContext();
            lock (Lock)
            {
                var model = mlContext.Model.Load(MlNetModelPath, out _);
                _engine = mlContext.Model.CreatePredictionEngine<InputModel, OutputModel>(model);
            }
        }

        [HttpPost]
        public ActionResult<OutputModel> Post([FromBody] InputModel input)
        {
            if (string.IsNullOrWhiteSpace(input.Text))
                return BadRequest();

            var prediction = _engine.Predict(input);

            var result = new
            {
                prediction.PredictedLabel,
                Scores = prediction.Score
            };

            return Ok(result);
        }
    }
}