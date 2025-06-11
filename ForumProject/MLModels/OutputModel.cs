namespace ForumProject.MLModels
{
    public class OutputModel
    {
        // The predicted label (e.g. toxic or not)
        public string PredictedLabel { get; set; }

        // The probability of the prediction being true
        public string Probability { get; set; }

        // The raw score from the model
        public string Score { get; set; }
    }
}