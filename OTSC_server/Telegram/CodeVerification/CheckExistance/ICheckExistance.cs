namespace OTSC_server.Telegram.CodeVerification;

public interface ICheckExistance
{
    public Task<bool>  CheckChatExistance(long chatId);
}