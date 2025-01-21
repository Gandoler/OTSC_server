namespace OTSC_server.Telegram.CodeVerification;

public class CodeSender
{
    private readonly IMessageSender _messageSender;
    private readonly VerificationCodeGenerator _codeGenerator;
    public int Code { get; set; }
    public CodeSender(IMessageSender messageSender, VerificationCodeGenerator codeGenerator)
    {
        _messageSender = messageSender ?? throw new ArgumentNullException(nameof(messageSender));
        _codeGenerator = codeGenerator ?? throw new ArgumentNullException(nameof(codeGenerator));
    }

    public async Task SendVerificationCodeAsync(long telegramId)
    {
        Code = _codeGenerator.GenerateCode();
        string message = $"Your code: {Code}";
        await _messageSender.SendMessageAsync(telegramId, message);
    }
}