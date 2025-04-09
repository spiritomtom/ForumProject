using Microsoft.ML.Data;

namespace ForumProject.Models
{
    public class CommentPrediction
    {
        [ColumnName("PredictedLabel")]
        public bool IsToxic { get; set; }
    }
}
