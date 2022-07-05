using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace StarTrekOrStarWars
{
    public static class PredictText
    {
        [FunctionName("PredictText/{text}")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            string text,
            ILogger log)
        {
            log.LogInformation("Executing text prediction...");

            if (string.IsNullOrWhiteSpace(text))
                return new BadRequestResult();

            return new OkResult();
        }
    }
}
