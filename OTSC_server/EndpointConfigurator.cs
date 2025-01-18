using OTSC_server.Telegram.CodeVerification;
using OTSC_server.Telegram.CodeVerification.CodeFactory;
using Serilog;

namespace OTSC_server;

public static class EndpointConfigurator
{
    public static void ConfigureEndpoints(WebApplication app)
    {
        app.MapGet("/VerificationCode/{id:long}", async (long id) =>
        {
            Console.WriteLine("кто-то подключился");
            CodeSenderFactory codeSenderFactory = new("7956821282:AAGfyHzlWYx4hg82r6dwbgTfhH8mX63PCFs");
            var chechexistchat = codeSenderFactory.CreateCheckExistance();
            bool userExists = await chechexistchat.CheckChatExistance(id);

            if (userExists)
            {
                CodeSender sender = codeSenderFactory.CreateCodeSender();
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
