using System;
using System.Linq;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

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
        [EventHubOutput("groupeventhub", Connection = "ehconnstring")]
        public string[] Run([EventHubTrigger("groupeventhub", Connection = "ehconnstring", ConsumerGroup = "$default", IsBatched = true)] string[] input)
        //public void Run([EventHubTrigger("groupeventhub", Connection = "ehconnstring", ConsumerGroup = "$default", IsBatched = true)] string[] input)
        {

            // Log all messages and contents
            foreach(var msg in input.ToList<string>())
            {
                _logger.LogInformation($"First Event Hubs triggered message: {msg}");
            }

            // Create String Array for Event Hub Output
            var myArray = new String[arraySize];

            // Loop through popularting myArray with DateTime
            for(int i = 0; i < arraySize; i++)
            {
                myArray[i] = DateTime.UtcNow.ToString("yyyy-MM-ddThh:mm:ss");
            }

            return myArray;
        }
    }
}
