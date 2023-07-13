//using System;
//using Microsoft.Azure.Functions.Worker;
//using Microsoft.Extensions.Logging;

//namespace EventHubGroup56FA
//{
//    public class ConsumerGroup2
//    {
//        private readonly ILogger _logger;

//        public ConsumerGroup2(ILoggerFactory loggerFactory)
//        {
//            _logger = loggerFactory.CreateLogger<ConsumerGroup2>();
//        }

//        [Function("ConsumerGroup2")]
//        public void Run([EventHubTrigger("groupeventhub", Connection = "ehconnstring", ConsumerGroup = "ConsumerGroup2", IsBatched = true)] string[] input)
//        {
//            _logger.LogInformation($"First Event Hubs triggered message: {input[0]}");
//        }
//    }
//}
