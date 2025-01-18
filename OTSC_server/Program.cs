using OTSC_server.Telegram;
using OTSC_server.Telegram.CodeVerification;
using OTSC_server.Telegram.CodeVerification.CodeFactory;
using Serilog;
internal static class Program
{
    public static void Main(string[] args)
    {
        Log.Logger = new LoggerConfiguration()
            .WriteTo.File(@"C:\Users\Николай\RiderProjects\OTSC_server\OTSC_server\Logs\myapp.log",
                rollingInterval: RollingInterval.Day)
            .CreateLogger();


        Log.Information("App start");
        var builder = WebApplication.CreateBuilder(args);


        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {

        }

        app.MapGet("/VerificationCode/{id:int}", async (int id) =>
        {
            CodeSenderFactory codeSenderFactory = new("7956821282:AAGfyHzlWYx4hg82r6dwbgTfhH8mX63PCFs");
            var chechexistchat = codeSenderFactory.CreateCheckExistance();
            bool userExists = await chechexistchat.CheckChatExistance(id);

            if (userExists)
            {

                CodeSender sender = codeSenderFactory.CreateCodeSender();


                await sender.SendVerificationCodeAsync(id);
                Log.Information($"All Good Code here");
                return Results.Ok(
                    new
                    {
                        Message = $"Verification Code {id}",
                        Code = sender.Code
                    });
            }
            else
            {
                Log.Information($"user{id} didnt subscribe");
                return Results.NotFound(new { Message = "User not found" });
            }
        });

        app.Run();
        
    }
}

