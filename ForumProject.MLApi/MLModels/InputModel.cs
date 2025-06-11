using Microsoft.ML.Data;

namespace ForumProject.MLApi.MLModels
{
    public class InputModel
    {
        // The text input for sentiment/toxicity analysis
        [LoadColumn(0)]
        public string Text { get; set; }

        [LoadColumn(1)]
        public string Label { get; set; }
    }
}