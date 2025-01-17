namespace OTSC_server.Telegram.CodeVerification;

public interface IMessageSender
{
    Task SendMessageAsync(long telegramId, string message);
}