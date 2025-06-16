namespace ForumProject.MLModels
{
    public class OutputModel
    {
        public string PredictedLabel { get; set; }

        public string Probability { get; set; }

        public float[] Score { get; set; }
    }
}