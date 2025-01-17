using OTSC_server.Telegram;
using OTSC_server.Telegram.CodeVerification;
using OTSC_server.Telegram.CodeVerification.CodeFactory;

var builder = WebApplication.CreateBuilder(args);


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
   
}

app.MapGet("/VerificationCode/{id:int}", async (int id) =>
{
    CodeSenderFactory codeSenderFactory = new("7956821282:AAGfyHzlWYx4hg82r6dwbgTfhH8mX63PCFs");
    var chechexistchat = codeSenderFactory.CreateCheckExistance();
    if (await chechexistchat.CheckChatExistance(id))
    {

        CodeSender sender = codeSenderFactory.CreateCodeSender();


        await sender.SendVerificationCodeAsync(id);

        return Results.Ok(
            new
            {
                Message = $"Verification Code {id}",
                Code = sender.Code
            });
    }
    else
    {
        return Results.NotFound(new { Message = "User not found" });
    }
});

app.Run();

