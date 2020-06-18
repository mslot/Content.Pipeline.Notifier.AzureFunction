using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using System;
using Microsoft.Azure.WebJobs.Extensions.EventGrid;
using Microsoft.Azure.EventGrid.Models;
using System.Collections.Generic;

namespace Content.Pipeline.Notifier.AzureFunction
{
    public class NotifierFunctions
    {
        [FunctionName("NotiferFunction")]
        [return: EventGrid(TopicEndpointUri = "EventGrid:TopicUri", TopicKeySetting = "EventGrid:TopicKey")]
        public Task Run(
        [ServiceBusTrigger("notifier-topic","NotifierSubscription", Connection = "Servicebus:ServicebusConnectionString")]
        string message,
        Int32 deliveryCount,
        DateTime enqueuedTimeUtc,
        string messageId,
        ILogger log)
        {
            log.LogInformation($"C# ServiceBus queue trigger function processed message: {message}");
            log.LogInformation($"EnqueuedTimeUtc={enqueuedTimeUtc}");
            log.LogInformation($"DeliveryCount={deliveryCount}");
            log.LogInformation($"MessageId={messageId}");

            var random = new Random();
            List<string> eventTypes = new List<string> { "UpSertMetadata", "UpsertTheme", "UpsertVideoUrl", "DeleteMetadata" };
            string eventType = eventTypes[random.Next(eventTypes.Count)];

            return Task.FromResult(new EventGridEvent(messageId, eventType, message, eventType, DateTime.UtcNow, "1.0"));
        }
    }
}
