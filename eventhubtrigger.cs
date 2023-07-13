using System;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace EventHubGroup56FA
{
    public class eventhubtrigger
    {
        private readonly ILogger _logger;

        public eventhubtrigger(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<eventhubtrigger>();
        }

        [Function("eventhubtrigger")]
        [EventHubOutput("groupeventhub", Connection = "ehconnstring")]
        public string[] Run([EventHubTrigger("groupeventhub", Connection = "ehconnstring", ConsumerGroup = "$default", IsBatched = true)] string[] input)
        //public void Run([EventHubTrigger("groupeventhub", Connection = "ehconnstring", ConsumerGroup = "$default", IsBatched = true)] string[] input)
        {
            _logger.LogInformation($"First Event Hubs triggered message: {input[0]}");

            var myArray = new String[5];

            for(int i = 0; i < 5; i++)
            {
                myArray[i] = DateTime.Now.ToString("yyyyMMddThh:mm:ss"); //i.ToString();
            }

            return myArray;
        }
    }
}
