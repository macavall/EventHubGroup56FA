using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;

internal class Program
{
    private static void Main(string[] args)
    {
        var host = new HostBuilder()
        .ConfigureFunctionsWorkerDefaults()
        .ConfigureServices( s =>
        {
            s.AddSingleton<IMyService, MyService>();
        })
        .Build();

        host.Run();
    }
}