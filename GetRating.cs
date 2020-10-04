using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;

namespace My.Functions
{
    public static class GetRatingFunctions
    {
        [FunctionName(nameof(GetRating))]
        public static IActionResult GetRating(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = null)] HttpRequest req,
            [CosmosDB(databaseName:@"%RatingsDbName%", collectionName:@"%RatingsCollectionName%", ConnectionStringSetting = @"RatingsDatabase")] IEnumerable<JObject> allRatings)
        {
            string ratingId = null;

            if (req.GetQueryParameterDictionary()?.TryGetValue(@"ratingId", out ratingId) == true
                && !string.IsNullOrWhiteSpace(ratingId))
            {
                var ratings = allRatings.Where(r => r.Value<string>(@"id") == ratingId);

                return !ratings.Any() ? new NotFoundObjectResult($@"No ratings found for ratingId '{ratingId}'") : (IActionResult)new OkObjectResult(ratings);

            }
            else
            {
                return new BadRequestObjectResult(@"ratingId is required as a query parameter");
            }
        }
    }
}