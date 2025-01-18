using OTSC_server;
using OTSC_server.Telegram;
using OTSC_server.Telegram.CodeVerification;
using OTSC_server.Telegram.CodeVerification.CodeFactory;
using Serilog;
using Serilog.Sinks.Async;
internal static class Program
{
    public static void Main(string[] args)
    {
        Log.Logger = new LoggerConfiguration()
            .WriteTo.Async(a => a.File(@"C:\Users\Николай\RiderProjects\OTSC_server\OTSC_server\Logs\myapp.log", 
                rollingInterval: RollingInterval.Month))
            .CreateLogger();

        Log.Information("App start");
        
        var builder = WebApplication.CreateBuilder(args);
        var app = builder.Build();

        
        EndpointConfigurator.ConfigureEndpoints(app);

        app.Run();
    }
}

