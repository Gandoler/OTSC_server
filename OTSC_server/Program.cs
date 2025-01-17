using OTSC_server.Telegram;

var builder = WebApplication.CreateBuilder(args);


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
   
}

app.MapGet("/VerificationCode/{id:int}", async (int id) =>
{
SendCode sender = new();
await sender.SendVerificationCode(id);

return Results.Ok(
    new
    {
        Message = $"Verification Code {id}",
        Code = sender.Code
    });
});

app.Run();

