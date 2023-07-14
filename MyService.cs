using Microsoft.Extensions.Logging;

public class MyService : IMyService
{
    public void MyServiceMethod(ILogger log)
    {
        log.LogInformation("Hello World!");
    }
}