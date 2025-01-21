

using OTSC_server.Telegram.CodeVerification;
using OTSC_server.Telegram.CodeVerification.CodeFactory;
using OTSC_ui.Tools.AppSettingJsonPhars.ConnectionStringManager;
using Serilog;

namespace OTSC_server;

public static class EndpointConfigurator
{
    private static readonly CodeSenderFactory _codeSenderFactory;
    private static readonly ICheckExistance _checkExistance;



    static EndpointConfigurator()
    {
        
        _codeSenderFactory = new(ConnectionStringManager.GetTgBotToken());
        _checkExistance = _codeSenderFactory.CreateCheckExistance();

    }
    
    public static void ConfigureEndpoints(WebApplication app)
    {
        app.MapGet("/VerificationCode/{id:long}", async (long id) =>
        {
            Console.WriteLine("кто-то подключился");
            
           
            bool userExists = await _checkExistance.CheckChatExistance(id);

            if (userExists)
            {
                CodeSender sender = _codeSenderFactory.CreateCodeSender();
                await sender.SendVerificationCodeAsync(id);

                Log.Information($"All Good Code here for user:{id}");
                return Results.Ok(new
                {
                    Message = $"Verification Code {id}",
                    IsSubscribed = true,
                    Code = sender.Code
                });
            }
            else
            {
                Log.Information($"user{id} didnt subscribe");
                return Results.NotFound(new { Message = "User not found", IsSubscribed = false });
            }
        });
    }
}
