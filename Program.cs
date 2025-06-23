using Lung.Analysis.System.ML;
using Lung.Analysis.System.Services;
using Microsoft.Extensions.ML;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add configuration
builder.Services.Configure<ModelConfig>(builder.Configuration.GetSection("ModelConfig"));

// Add ML.NET services
builder.Services.AddPredictionEnginePool<ImageInputData, ImagePrediction>()
    .FromFile(builder.Configuration["ModelConfig:ModelPath"]);

// Add our ML service
builder.Services.AddScoped<IMLService, MLService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
