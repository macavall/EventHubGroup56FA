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
        public List<EventData> Run([EventHubTrigger("%EventHubName%", Connection = "ehconnstring", ConsumerGroup = "$Default", IsBatched = true)] string[] input)
        //public void Run([EventHubTrigger("groupeventhub", Connection = "ehconnstring", ConsumerGroup = "$default", IsBatched = true)] string[] input)
        {
            bool started = false;


            //_logger.LogInformation($"First Event Hubs triggered message: {input[0].PartitionKey}");

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


                //var msgString = msg.EventBody.ToString();

                //_logger.LogInformation($"First Event Hubs triggered message: {msg}");

                //for (int i = 0; i < arraySize; i++)
                //{
                //    //var newEvent = new EventData();

                var jsonObject = new
                {
                    dateTime = DateTime.UtcNow.ToString("yyyyMMddThhmmss")
                };

                //    //string dateTime = DateTime.UtcNow.ToString("yyyyMMddThhmmss");

                string dateTimeJson = Newtonsoft.Json.JsonConvert.SerializeObject(jsonObject);

                //    //byte[] binaryData = Encoding.UTF8.GetBytes(dateTimeJson);

                //    //BinaryData finalBinaryData = new BinaryData(binaryData);

                //    //newEvent.EventBody = finalBinaryData;

                //    //System.Threading.Thread.Sleep(100);

                //    var keep = temp;

                //    if (started == false)
                //    {
                //        var pretemp = Newtonsoft.Json.JsonConvert.DeserializeObject(temp);

                //        var fintemp = Newtonsoft.Json.JsonConvert.SerializeObject(pretemp);

                eventDataList.Add(new EventData()
                {
                    EventBody = new BinaryData(Encoding.UTF8.GetBytes(dateTimeJson))
                });

                //        started = true;
                //    }

                //    eventDataList.Add(tempEventData);

                //    //eventDataList.Add(new EventData()
                //    //{
                //    //    EventBody = new BinaryData(Encoding.UTF8.GetBytes("{}"))
                //    //});

                //    //DateTime.UtcNow.ToString("yyyyMMddThhmmss"));
                //}
            }
            //// Create String Array for Event Hub Output
            //var myArray = new String[arraySize];

            //// Loop through popularting myArray with DateTime
            //for(int i = 0; i < arraySize; i++)
            //{
            //    myArray[i] = DateTime.UtcNow.ToString("yyyy-MM-ddThh:mm:ss");
            //}
            //var myArrayLength = eventDataList.Count;

            //for(int i = 0; i < myArrayLength; i++)
            //{

            //}

            return eventDataList;
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
