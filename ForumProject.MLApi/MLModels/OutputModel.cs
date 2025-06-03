namespace ForumProject.MLModels
{
    public class OutputModel
    {
        // The predicted label (e.g. toxic or not)
        public bool PredictedLabel { get; set; }

        // The probability of the prediction being true
        public float Probability { get; set; }

        // The raw score from the model
        public float Score { get; set; }
    }
}