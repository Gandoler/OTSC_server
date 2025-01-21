using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Types;

namespace OTSC_server.Telegram.CodeVerification;

public class CheckExistance:ICheckExistance
{
    private readonly TelegramBotClient _botClient;

    public CheckExistance(TelegramBotClient botClient)
    {
        this._botClient = botClient;
    }

    public async Task<bool>  CheckChatExistance(long chatId)
    {
        try
        {
            Chat? chat = await this._botClient.GetChat(chatId);
            return chat != null;
        }
        catch (ApiRequestException ex) when (ex.ErrorCode == 400)
        {
            
            return false;
        }
        catch (Exception ex)
        {
            
            Console.WriteLine($"Ошибка: {ex.Message}");
            return false;
        }
    }
}