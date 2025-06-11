using Microsoft.ML.Data;

namespace ForumProject.MLApi.MLModels
{
    public class OutputModel
    {
        [ColumnName("PredictedLabel")]
        public bool PredictedLabel { get; set; }

        [ColumnName("Score")]
        public float[] Score { get; set; }
    }
}