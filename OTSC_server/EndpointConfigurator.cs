

using ClassLibrary1.tools.SendMailWithcode.RequestTemplates;
using OTSC_server.Telegram.CodeVerification;
using OTSC_server.Telegram.CodeVerification.CodeFactory;
using OTSC_ui.Tools.AppSettingJsonPhars.ConnectionStringManager;
using OTSC_ui.Tools.HTTPqUERY;
using OTSC_ui.Tools.SendMailWithcode.CodeGenerate;
using OTSC_ui.Tools.SendMailWithcode.CodeSend;
using Serilog;

namespace OTSC_server;

public static class EndpointConfigurator
{
    private static readonly CodeSenderFactory _codeSenderFactory;
    private static readonly ICheckExistance _checkExistance;
    private static readonly ICodeGeneratorMail _codeGeneratorMail;
    private static readonly EmailServiceWithTemplate _emailServiceWithTemplate;


    static EndpointConfigurator()
    {
        
        _codeSenderFactory = new(ConnectionStringManager.GetTgBotToken());
        _checkExistance = _codeSenderFactory.CreateCheckExistance();
        _codeGeneratorMail = new CodeGeneratorsix();
        _emailServiceWithTemplate = new EmailServiceWithTemplate(ConnectionStringManager.GetEmailSettings());

    }
    
    public static void ConfigureEndpoints(WebApplication app)
    {
        app.MapPost("/VerificationCode", async (VerificationRequestCodeSend verificationRequest) =>
        {
            Console.WriteLine("connect in verification code");
            
           
            bool userExists = await _checkExistance.CheckChatExistance(verificationRequest.Id);

            if (userExists)
            {
                CodeSender sender = _codeSenderFactory.CreateCodeSender();
                await sender.SendVerificationCodeAsync(verificationRequest.Id);

                Log.Information($"All Good Code here for user:{verificationRequest.Id} in {nameof(ConfigureEndpoints)}");
                return Results.Ok(new VerificationResponseCodeSend($"Verification Code {verificationRequest.Id}",
                    sender.Code, true));

            }
            else
            {
                Log.Information($"user{verificationRequest.Id} didnt subscribe");
                return Results.NotFound(new VerificationResponseCodeSend($"User not found {verificationRequest.Id}" , false ));
            }
        });


        app.MapPost("/SendMail", async (ChangePassRequest changePassRequest ) =>
        {
            Console.WriteLine("connect in SendMail code");
            _codeGeneratorMail.GenerateCode();
            await _emailServiceWithTemplate.SendEmailAsync(changePassRequest.Email , "You code", $"{_codeGeneratorMail.GenerateCode()}");
            Log.Information($"All Good Code here for user email:{changePassRequest.Email} in {nameof(ConfigureEndpoints)}");

            return Results.Ok();
        });
    }
}
