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

        // If arraySize is null, set default to 5, otherwise get the environment variable value

        //private readonly int arraySize = Convert.ToInt32(Environment.GetEnvironmentVariable("arraySize")) 
        private readonly int arraySize = Convert.ToInt32(Environment.GetEnvironmentVariable("arraySize")) > 0 ? Convert.ToInt32(Environment.GetEnvironmentVariable("arraySize")) : 5 ;

        public eventhubtrigger(ILoggerFactory loggerFactory, IMyService _myService)
        {
            _logger = loggerFactory.CreateLogger<eventhubtrigger>();
            myService = _myService;
        }

        [Function("eventhubtrigger")]
        [EventHubOutput("%EventHubName%", Connection = "ehconnstring")]
        public string[] Run([EventHubTrigger("%EventHubName%", Connection = "ehconnstring", ConsumerGroup = "$Default", IsBatched = true)] string[] input)
        //public void Run([EventHubTrigger("groupeventhub", Connection = "ehconnstring", ConsumerGroup = "$default", IsBatched = true)] string[] input)
        {
            //_logger.LogInformation($"First Event Hubs triggered message: {input[0].PartitionKey}");

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
            var eventDataList = new List<string>();

            // Log all messages and contents
            foreach(var msg in input)
            {
                //var msgString = msg.EventBody.ToString();

                _logger.LogInformation($"First Event Hubs triggered message: {msg}");

                for (int i = 0; i < arraySize; i++)
                {
                    //var newEvent = new EventData();

                    //var jsonObject = new
                    //{
                    //    dateTime = DateTime.UtcNow.ToString("yyyyMMddThhmmss")
                    //};

                    //string dateTime = DateTime.UtcNow.ToString("yyyyMMddThhmmss");

                    //string dateTimeJson = Newtonsoft.Json.JsonConvert.SerializeObject(jsonObject);

                    //byte[] binaryData = Encoding.UTF8.GetBytes(dateTimeJson);

                    //BinaryData finalBinaryData = new BinaryData(binaryData);

                    //newEvent.EventBody = finalBinaryData;

                    eventDataList.Add(DateTime.UtcNow.ToString("yyyyMMddThhmmss"));
                }
            }

            //// Create String Array for Event Hub Output
            //var myArray = new String[arraySize];

            //// Loop through popularting myArray with DateTime
            //for(int i = 0; i < arraySize; i++)
            //{
            //    myArray[i] = DateTime.UtcNow.ToString("yyyy-MM-ddThh:mm:ss");
            //}

            var myArrayLength = eventDataList.Count;

            for(int i = 0; i < myArrayLength; i++)
            {

            }

            return eventDataList.ToArray();
        }

        public class TestClass
        {
            public TestClass()
            {

            }

            public void TestClassMethod()
            {
                Console.WriteLine("Hello World");
            }
        }
    }
}
