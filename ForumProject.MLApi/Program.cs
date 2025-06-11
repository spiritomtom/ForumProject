using ForumProject.MLApi;
using ForumProject.MLApi.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<SentimentPredictionService>();
builder.Services.AddControllers();

var app = builder.Build();

string dataPath = "D:\\School\\ForumProject\\ForumProject.MLApi\\sentiment_data.csv";
string modelPath = "MLModel.mlnet";

MlModelTrainer.TrainAndSaveModel(dataPath, modelPath);
app.MapControllers();

app.Run();

