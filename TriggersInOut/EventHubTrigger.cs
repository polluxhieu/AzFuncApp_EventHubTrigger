using Microsoft.Azure.EventHubs;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace TriggersInOut
{
    public static class EventHubTrigger
    {
        [FunctionName("EventHubDemoTrigger")]
        public static void Run(
            [EventHubTrigger("group3-demo-eh", Connection = "EventHubConnection")]EventData data, ILogger log,
            [CosmosDB("group3-triggers-inoutbindings", "eventshub", ConnectionStringSetting = "CosmosDbConnection")] IAsyncCollector<EventResult> result)
        {
            log.LogInformation($"Event Hub Demo Trigger processed a message: {data}");

            // Metadata accessed by binding to EventData
            log.LogInformation($"EnqueuedTimeUtc={data.SystemProperties.EnqueuedTimeUtc}");
            log.LogInformation($"SequenceNumber={data.SystemProperties.SequenceNumber}");
            log.LogInformation($"Offset={data.SystemProperties.Offset}");

            var eventResult = new EventResult
            {
                EnqueuedTimeUtc = data.SystemProperties.EnqueuedTimeUtc,
                SequenceNumber = data.SystemProperties.SequenceNumber,
                Offset = data.SystemProperties.Offset
            };
            result.AddAsync(eventResult);
        }
    }
}
