using Telegram.Bot;

namespace OTSC_server.Telegram;

public class TelegramBotContext
{
    #region staticPart

    private static readonly TelegramBotClient Botclient;
    
    static TelegramBotContext()
    {
        var botToken = GetBotToken();
        Botclient = new TelegramBotClient(botToken);
    }
    private static string GetBotToken()
    {
        return "7956821282:AAGfyHzlWYx4hg82r6dwbgTfhH8mX63PCFs";
    }
    #endregion

    public TelegramBotClient BotClient => Botclient;
}