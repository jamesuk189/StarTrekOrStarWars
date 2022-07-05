using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Microsoft.ML;
using Microsoft.ML.Data;
using Microsoft.Extensions.ML;

namespace StarTrekOrStarWars
{
    public class PredictText
    {

        private readonly PredictionEnginePool<ModelInput, ModelOutput> _predictionEnginePool;

        public PredictText(PredictionEnginePool<ModelInput, ModelOutput> predictionEnginePool)
        {
            _predictionEnginePool = predictionEnginePool;
        }

        public class ModelInput
        {
            [ColumnName(@"Text")]
            public string Text { get; set; }

            [ColumnName(@"Label")]
            public string Label { get; set; }
        }

        public class ModelOutput
        {
            [ColumnName(@"Text")]
            public float[] Text { get; set; }

            [ColumnName(@"Label")]
            public uint Label { get; set; }

            [ColumnName(@"Features")]
            public float[] Features { get; set; }

            [ColumnName(@"PredictedLabel")]
            public string PredictedLabel { get; set; }

            [ColumnName(@"Score")]
            public float[] Score { get; set; }
        }

        public class HttpInput
        {
            public string Text { get; set; }
        }

        [FunctionName("PredictText")]
        public IActionResult Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] HttpInput req,
            ILogger log)
        {
            if (req is null || string.IsNullOrWhiteSpace(req.Text))
                return new BadRequestResult();

            try
            {
                ModelOutput output = _predictionEnginePool.Predict(new ModelInput { Text = req.Text });

                var result = new
                {
                    Label = output.PredictedLabel
                };

                return new JsonResult(result);
            }
            catch (System.Exception ex)
            {
                return new JsonResult(ex);
            }
        }
    }
}
