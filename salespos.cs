using Microsoft.Azure.WebJobs;
using Microsoft.Azure.EventHubs;
using Microsoft.Extensions.Logging;

namespace My.Functions
{
    public static class POSSales
    {
        [FunctionName("POSSales")]
        public static void Run([EventHubTrigger("sohchallengeeventhub", Connection = "Endpoint=sb://sohchallenge.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=hx/dblFAlO5bg6IdeBe07bAraDP1ouZPfbjszyzezyU=")] string myEventHubMessage, ILogger log)
        {
            log.LogInformation($"C# function triggered to process a message: {myEventHubMessage}");
        }
    } 
}
