using ForumProject.MLApi.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<SentimentPredictionService>();
builder.Services.AddControllers();

var app = builder.Build();

app.MapControllers();

app.Run();

