using Microsoft.ML;

namespace ForumProject.MLApi
{
    public class MlModelTrainer
    {
        public static IEstimator<ITransformer> BuildPipeline(MLContext mlContext)
        {
            return mlContext.Transforms.Conversion.MapValueToKey(outputColumnName: "Label", inputColumnName: "Label")
                .Append(mlContext.Transforms.Text.FeaturizeText("Features", "Text"))
                .Append(mlContext.MulticlassClassification.Trainers.SdcaMaximumEntropy())
                .Append(mlContext.Transforms.Conversion.MapKeyToValue("PredictedLabel", "PredictedLabel"));
        }

        public static void TrainAndSaveModel(string dataPath, string modelPath)
        {
            var mlContext = new MLContext();

            var data = mlContext.Data.LoadFromTextFile<MLModels.InputModel>(dataPath, separatorChar: ',', hasHeader: true);

            var pipeline = BuildPipeline(mlContext);

            var model = pipeline.Fit(data);

            mlContext.Model.Save(model, data.Schema, modelPath);
        }
    }
}