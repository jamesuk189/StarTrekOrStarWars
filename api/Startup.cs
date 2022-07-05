using System.IO;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.ML;
using SentimentAnalysisFunctionsApp;
using static StarTrekOrStarWars.PredictText;

[assembly: FunctionsStartup(typeof(Startup))]
namespace SentimentAnalysisFunctionsApp
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            string modelPath = Path.Combine(builder.GetContext().ApplicationRootPath, "Model.zip");

            builder.Services.AddPredictionEnginePool<ModelInput, ModelOutput>()
                .FromFile(modelPath);
        }
    }
}