

using OTSC_server.Telegram.CodeVerification;
using OTSC_server.Telegram.CodeVerification.CodeFactory;
using OTSC_ui.Tools.AppSettingJsonPhars.ConnectionStringManager;
using OTSC_ui.Tools.HTTPqUERY;
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
        app.MapPost("/VerificationCode", async (VerificationRequestCodeSend verificationRequest) =>
        {
            Console.WriteLine("кто-то подключился");
            
           
            bool userExists = await _checkExistance.CheckChatExistance(verificationRequest.Id);

            if (userExists)
            {
                CodeSender sender = _codeSenderFactory.CreateCodeSender();
                await sender.SendVerificationCodeAsync(verificationRequest.Id);

                Log.Information($"All Good Code here for user:{verificationRequest.Id}");
                return Results.Ok(new VerificationResponseCodeSend($"Verification Code {verificationRequest.Id}",
                    sender.Code, true));

            }
            else
            {
                Log.Information($"user{verificationRequest.Id} didnt subscribe");
                return Results.NotFound(new VerificationResponseCodeSend($"User not found {verificationRequest.Id}" , false ));
            }
        });


        app.MapPost("/SendMail", async (VerificationRequestCodeSend verificationRequest) =>
        {
            
        });
    }
}
