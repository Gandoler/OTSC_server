namespace OTSC_server.Telegram.CodeVerification;

public class VerificationCodeGenerator
{
    private static readonly Random _random = new();

    public int GenerateCode()
    {
        return _random.Next(1000, 9999);
    }
}