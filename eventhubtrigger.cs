using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Azure.Messaging.EventHubs;
using System.Text;
using System.Collections.ObjectModel;
using System.Text.Json.Serialization;
using Newtonsoft;

namespace EventHubGroup56FA
{
    public class eventhubtrigger
    {
        private readonly ILogger _logger;
        private readonly IMyService myService;
        private readonly string? machineName = Environment.GetEnvironmentVariable("WEBSITE_HOSTNAME");
        public EventData tempEventData;
        private readonly int arraySize = Convert.ToInt32(Environment.GetEnvironmentVariable("arraySize")) > 0 ? Convert.ToInt32(Environment.GetEnvironmentVariable("arraySize")) : 5 ;

        public eventhubtrigger(ILoggerFactory loggerFactory, IMyService _myService)
        {
            _logger = loggerFactory.CreateLogger<eventhubtrigger>();
            myService = _myService;
        }

        [Function("eventhubtrigger")]
        [EventHubOutput("%EventHubName%", Connection = "ehconnstring")]
        public List<EventData> Run([EventHubTrigger("%EventHubName%", Connection = "ehconnstring", ConsumerGroup = "$Default", IsBatched = true)] string[] input)
        {
            bool started = false;

            string temp = input[0];

            try
            {
                if (machineName != null)
                {
                    _logger.LogInformation($"MachineName: {machineName}");
                }
            }
            catch(Exception ex)
            {
                _logger.LogInformation($"Error: {ex.Message}");
            }

            // Create a List of Type String
            var eventDataList = new List<EventData>();

            // Log all messages and contents
            for(int x = 0; x < arraySize*5; x++)
            {
                var jsonObject = new
                {
                    dateTime = DateTime.UtcNow.ToString("yyyyMMddThhmmss")
                };

                string dateTimeJson = Newtonsoft.Json.JsonConvert.SerializeObject(jsonObject);

                eventDataList.Add(new EventData()
                {
                    EventBody = new BinaryData(Encoding.UTF8.GetBytes(dateTimeJson))
                });
            }

            return eventDataList;
        }
    }
}
