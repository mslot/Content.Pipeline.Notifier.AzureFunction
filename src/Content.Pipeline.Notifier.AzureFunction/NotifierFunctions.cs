using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using System;

namespace Content.Pipeline.Notifier.AzureFunction
{
    public class NotifierFunctions
    {
        [FunctionName("NotiferFunction")]
        public void Run(
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

        }
    }
}
