using OTSC_server;

using Serilog;
using Serilog.Sinks.Async;
internal static class Program
{
    public static void Main(string[] args)
    {
        Log.Logger = new LoggerConfiguration()
            .WriteTo.Async(a => a.File(@"Logs\myapp.log", 
                rollingInterval: RollingInterval.Month))
            .CreateLogger();

        Log.Information("App start");
        
        var builder = WebApplication.CreateBuilder(args);
        var app = builder.Build();

        
        EndpointConfigurator.ConfigureEndpoints(app);

        app.Run();
    }
}

