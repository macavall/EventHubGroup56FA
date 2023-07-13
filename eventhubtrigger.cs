using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Azure.Messaging.EventHubs;
using System.Text;

namespace EventHubGroup56FA
{
    public class eventhubtrigger
    {
        private readonly ILogger _logger;

        // If arraySize is null, set default to 5, otherwise get the environment variable value

        //private readonly int arraySize = Convert.ToInt32(Environment.GetEnvironmentVariable("arraySize")) 
        private readonly int arraySize = Convert.ToInt32(Environment.GetEnvironmentVariable("arraySize")) > 0 ? Convert.ToInt32(Environment.GetEnvironmentVariable("arraySize")) : 5 ;

        public eventhubtrigger(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<eventhubtrigger>();
        }

        [Function("eventhubtrigger")]
        [EventHubOutput("%EventHubName%", Connection = "ehconnstring")]
        public EventData[] Run([EventHubTrigger("%EventHubName%", Connection = "ehconnstring", ConsumerGroup = "$Default", IsBatched = true)] EventData[] input)
        //public void Run([EventHubTrigger("groupeventhub", Connection = "ehconnstring", ConsumerGroup = "$default", IsBatched = true)] string[] input)
        {
            _logger.LogInformation($"First Event Hubs triggered message: {input[0].PartitionKey}");

            // Create a List of Type String
            var eventDataList = new List<EventData>();

            // Log all messages and contents
            foreach(var msg in input.ToList<EventData>())
            {
                var msgString = msg.EventBody.ToString();

                _logger.LogInformation($"First Event Hubs triggered message: {msgString}");

                for (int i = 0; i < arraySize; i++)
                {
                    var newEvent = new EventData();

                    string dateTime = DateTime.UtcNow.ToString("yyyy-MM-ddThh:mm:ss");

                    byte[] binaryData = Encoding.UTF8.GetBytes(dateTime);

                    BinaryData finalBinaryData = new BinaryData(binaryData);

                    newEvent.EventBody = finalBinaryData;

                    eventDataList.Add(newEvent);
                }
            }

            //// Create String Array for Event Hub Output
            //var myArray = new String[arraySize];

            //// Loop through popularting myArray with DateTime
            //for(int i = 0; i < arraySize; i++)
            //{
            //    myArray[i] = DateTime.UtcNow.ToString("yyyy-MM-ddThh:mm:ss");
            //}

            return eventDataList.ToArray();
        }
    }
}
