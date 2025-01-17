using Telegram.Bot;
using Telegram.Bot.Types;

namespace OTSC_server.Telegram;

public class SendCode
{
    public int Code { get; set; }
    
    public async Task SendVerificationCode(long telegramId)
    {
        GenerateCode();
        TelegramBotContext botinok = new();
        await botinok.BotClient.SendMessage(
            chatId: new ChatId(telegramId),
            text: $"your code: {this.Code}"); 
    }

    private void GenerateCode()
    {
        Random random = new();
        Code = random.Next(1000, 9999);
    }
}