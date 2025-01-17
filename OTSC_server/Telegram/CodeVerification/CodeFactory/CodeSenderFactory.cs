using Telegram.Bot;

namespace OTSC_server.Telegram.CodeVerification.CodeFactory;

public class CodeSenderFactory
{
    private readonly TelegramBotClient _botClient;

    public CodeSenderFactory(string botToken)
    {
        _botClient = new TelegramBotClient(botToken) ?? throw new ArgumentNullException(nameof(botToken));
    }

    public CodeSender CreateCodeSender()
    {
        // Создаем экземпляры нужных классов через фабрику
        var messageSender = new TelegramMessageSender(_botClient);
        var codeGenerator = new VerificationCodeGenerator();
        return new CodeSender(messageSender, codeGenerator);
    }

    public CheckExistance CreateCheckExistance()
    {
       
        return new CheckExistance(_botClient);
    }
}