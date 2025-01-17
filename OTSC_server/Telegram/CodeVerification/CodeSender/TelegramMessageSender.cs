using Telegram.Bot;
using Telegram.Bot.Types;

namespace OTSC_server.Telegram.CodeVerification;

public class TelegramMessageSender:IMessageSender
{
    private readonly TelegramBotClient _botClient;

    public TelegramMessageSender(TelegramBotClient botClient)
    {
        _botClient = botClient;
    }

    public async Task SendMessageAsync(long telegramId, string message)
    {
        await _botClient.SendMessage(
            chatId: new ChatId(telegramId),
            text: message);
    }
}