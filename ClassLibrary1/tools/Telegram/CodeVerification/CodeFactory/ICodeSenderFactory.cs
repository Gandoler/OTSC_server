namespace OTSC_server.Telegram.CodeVerification.CodeFactory;

public interface ICodeSenderFactory
{
    CodeSender CreateCodeSender();
}